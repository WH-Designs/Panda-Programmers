using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.DAL.Abstract;
using Microsoft.AspNetCore.Authentication;
using SendGrid;
using SendGrid.Helpers.Mail;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.ViewModels;

namespace MusicCollaborationManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IListenerRepository _listenerRepository;
    private readonly SpotifyAuthService _spotifyService;
    private readonly IYouTubeService _youTubeService;
    private readonly ISpotifyAuthorizationNeededRepository _spotifyAuthNeededRepository;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository
, IYouTubeService youTubeService, ISpotifyAuthorizationNeededRepository spotifyAuthNeededRepository)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyService = spotifyService;
        _listenerRepository = listenerRepository;
        _youTubeService = youTubeService;
        _spotifyAuthNeededRepository = spotifyAuthNeededRepository;
    }

    public async Task<IActionResult> Index()
    {
        ViewBag.Error = TempData["Error"];
        
        IEnumerable<MusicVideoDTO> j = await _youTubeService.GetPopularMusicVideosAsync();
        VisitorDashboard visitorDash = new VisitorDashboard();
        foreach (MusicVideoDTO video in j)
        {
            visitorDash.YouTubeMVs.Add(video);
        }

        return View(visitorDash);
    }

    public async Task<IActionResult> Whoops(Listener listener)
    {

        try {
            SpotifyAuthorizationNeededListener empty_authNeededListener = new SpotifyAuthorizationNeededListener();
            WhoopsViewModel viewModel = new WhoopsViewModel();
            viewModel.listener = listener;
            viewModel.authNeededListener = empty_authNeededListener;
            return View(viewModel);

        } catch (Exception ex) {
            Console.WriteLine("Error: " + ex.Message);
            ViewBag.Error = "Error Occurred";

            IEnumerable<MusicVideoDTO> j = await _youTubeService.GetPopularMusicVideosAsync();
            VisitorDashboard visitorDash = new VisitorDashboard();
            foreach (MusicVideoDTO video in j)
            {
                visitorDash.YouTubeMVs.Add(video);
            }

            return View("Index", visitorDash);
        }
        
    }

    [HttpPost]
    public async Task<IActionResult> PostWhoops(WhoopsViewModel viewModel)
    {
        try {
            SpotifyAuthorizationNeededListener listener_to_pass = viewModel.authNeededListener;
            var all_auth_listeners = _spotifyAuthNeededRepository.GetAll().ToList();
            
            var current_listener_auth = all_auth_listeners.Where(l => l.ListenerId == listener_to_pass.ListenerId).ToList();

            if (current_listener_auth != null) {
                ViewBag.Exists = "True";
                IEnumerable<MusicVideoDTO> ja = await _youTubeService.GetPopularMusicVideosAsync();
                VisitorDashboard visitorDash2 = new VisitorDashboard();
                foreach (MusicVideoDTO video in ja)
                {
                    visitorDash2.YouTubeMVs.Add(video);
                }

                return View("Index", visitorDash2);
            } else {
                _spotifyAuthNeededRepository.AddOrUpdateSpotifyAuthListener(listener_to_pass);
                ViewBag.Passed = "Passed";

                IEnumerable<MusicVideoDTO> j = await _youTubeService.GetPopularMusicVideosAsync();
                VisitorDashboard visitorDash = new VisitorDashboard();
                foreach (MusicVideoDTO video in j)
                {
                    visitorDash.YouTubeMVs.Add(video);
                }

                return View("Index", visitorDash);    
            }
        } catch(Exception ex) {
            Console.WriteLine("Error: " + ex.Message);
            ViewBag.Error = "Error Occurred";
            IEnumerable<MusicVideoDTO> j = await _youTubeService.GetPopularMusicVideosAsync();
            VisitorDashboard visitorDash = new VisitorDashboard();
            foreach (MusicVideoDTO video in j)
            {
                visitorDash.YouTubeMVs.Add(video);
            }

            return View("Index", visitorDash);    
        }
    }

    public IActionResult callforward()
    {
        try {
            string aspId = _userManager.GetUserId(User);
            Listener listener = new Listener();
            listener = _listenerRepository.FindListenerByAspId(aspId);

            if (listener.AuthToken == null)
            {
                String uri = _spotifyService.GetUriAsync();
                return Redirect(uri);
            }

            return RedirectToAction("callback", "Home", "");
        } catch(Exception ex) {
            Console.WriteLine("Error: " + ex.Message);
            ViewBag.Error = "Error Occurred";
            return View("Index");
        }

        
    }

    public async Task<IActionResult> callback(string code)
    {
        try {
            string aspId = _userManager.GetUserId(User);
            Listener listener = new Listener();
            listener = _listenerRepository.FindListenerByAspId(aspId);

            await _spotifyService.GetCallbackAsync(code, listener);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(listener);
            PrivateUser currentSpotifyUser = await _spotifyService.GetAuthUserAsync(spotifyClient);
            listener.SpotifyUserName = currentSpotifyUser.DisplayName;
            _listenerRepository.AddOrUpdate(listener);

            if (listener.SpotifyId == null)
            {
                listener.SpotifyId = currentSpotifyUser.Id;
                _listenerRepository.AddOrUpdate(listener);
            }

            return RedirectToAction("Index", "Listener");
        } catch(SpotifyAPI.Web.APIException) {
            string aspId = _userManager.GetUserId(User);
            Listener listener = new Listener();
            listener = _listenerRepository.FindListenerByAspId(aspId);
            
            SpotifyAuthorizationNeededListener empty_authNeededListener = new SpotifyAuthorizationNeededListener();
            WhoopsViewModel viewModel = new WhoopsViewModel();
            viewModel.listener = listener;
            viewModel.authNeededListener = empty_authNeededListener;

            return View("Whoops", viewModel);
        }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}


