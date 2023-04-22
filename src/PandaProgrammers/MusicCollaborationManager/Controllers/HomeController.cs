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

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository
, IYouTubeService youTubeService)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyService = spotifyService;
        _listenerRepository = listenerRepository;
        _youTubeService = youTubeService;
    }

    public async Task<IActionResult> Index()
    {
        //IEnumerable<MusicVideoDTO> j = await _youTubeService.GetPopularMusicVideosAsync();
        VisitorDashboard visitorDash = new VisitorDashboard();

        Console.WriteLine("---------Printing all YouTube video details-------------");

        int i = 1;
        //foreach (MusicVideoDTO video in j)
        //{
        //    Console.WriteLine($"---------Video {i}-------------");
        //    Console.WriteLine($"Title: {video.VideoTitle}");
        //    Console.WriteLine($"YouTube Channel : {video.YouTubeChannelName}");
        //    Console.WriteLine($"Thumbnail URL : {video.ThumbnailURL}");
        //    Console.WriteLine($"Thumbnail height : {video.ThumbnailHeight}");
        //    Console.WriteLine($"Thumbnail width : {video.ThumbnailWidth}");
        //    Console.WriteLine($"Channel ID: {video.VideoID}");
        //    i++;

        //    visitorDash.YouTubeMVs.Add(video);
        //}

        return View(visitorDash);
    }

    public IActionResult callforward()
    {
        string aspId = _userManager.GetUserId(User);
        Listener listener = new Listener();
        listener = _listenerRepository.FindListenerByAspId(aspId);

        if (listener.AuthToken == null)
        {
            String uri = _spotifyService.GetUriAsync();
            return Redirect(uri);
        }

        return RedirectToAction("callback", "Home", "");
    }

    public async Task<IActionResult> callback(string code)
    {
        string aspId = _userManager.GetUserId(User);
        Listener listener = new Listener();
        listener = _listenerRepository.FindListenerByAspId(aspId);

        await _spotifyService.GetCallbackAsync(code, listener);
        PrivateUser currentSpotifyUser = await _spotifyService.GetAuthUserAsync();
        _listenerRepository.AddOrUpdate(listener);

        if (listener.SpotifyId == null)
        {
            listener.SpotifyId = currentSpotifyUser.Id;
            _listenerRepository.AddOrUpdate(listener);
        }

        return RedirectToAction("Index", "Listener");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



