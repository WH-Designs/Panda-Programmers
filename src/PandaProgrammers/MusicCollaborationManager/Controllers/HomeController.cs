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


namespace MusicCollaborationManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IListenerRepository _listenerRepository;
    private readonly SpotifyAuthService _spotifyService;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyService = spotifyService;
        _listenerRepository = listenerRepository;
    }
    
    public IActionResult Index()
    {
        
        return View();
    }

    public IActionResult callforward()
    {
        String uri = _spotifyService.GetUri();
        return Redirect(uri);
    }

    public async Task<IActionResult> callback(string code)
    {
        string aspId = _userManager.GetUserId(User);
        Listener listener = new Listener();
        listener = _listenerRepository.FindListenerByAspId(aspId);        
        
        await _spotifyService.GetCallback(code);
        PrivateUser currentSpotifyUser = await _spotifyService.GetAuthUser();

        if (listener.SpotifyId == null) {
            listener.SpotifyId = currentSpotifyUser.Id;
            _listenerRepository.AddOrUpdate(listener);
        } else if (listener.SpotifyId != currentSpotifyUser.Id) {
            HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
            //return Redirect("https://open.spotify.com/");
            // AuthenticationProperties authentication = new AuthenticationProperties();
            // AuthenticationService.SignOutAsync(HttpContext, "Spotify", authentication);
        }

        //HttpContext.Connection.RequestClose();
        
        //database saved spotify user id == new user id?
        //if not : give a logout of spotify<--
        //if is proceed:
        //logout of spotify elsewhere

        return RedirectToAction("Index", "Listener");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



