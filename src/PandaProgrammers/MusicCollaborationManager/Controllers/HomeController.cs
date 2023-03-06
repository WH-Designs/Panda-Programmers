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
        string aspId = _userManager.GetUserId(User);
        Listener listener = new Listener();
        listener = _listenerRepository.FindListenerByAspId(aspId);

        if (listener.AuthToken == null){
            String uri = _spotifyService.GetUri();
            return Redirect(uri);  
        }
        
        return RedirectToAction("callback", "Home", "");
    }

    public async Task<IActionResult> callback(string code)
    {
        // grabs all the current spotify accounts that have spotify ids in the db
        List<Listener> currentSpotifyAccountsWithSpotifyIDs = _listenerRepository.GetAll().Where(a => a.SpotifyId != null).ToList();

        // gets current user account
        string aspId = _userManager.GetUserId(User);
        Listener listener = new Listener();
        listener = _listenerRepository.FindListenerByAspId(aspId);        
        
        // goes and does the callback which grabs the token after the user has signed into spotify (which is the callforward)
        await _spotifyService.GetCallback(code, listener);
        PrivateUser currentSpotifyUser = await _spotifyService.GetAuthUser();
        _listenerRepository.AddOrUpdate(listener);

        // // checks in the current listener has the same spotify account as someone else in the db that isnt the current listener and sends them away if they are
        // foreach(Listener account in currentSpotifyAccountsWithSpotifyIDs) {
        //     if (listener.SpotifyId == account.SpotifyId && listener.FirstName + listener.LastName != account.FirstName + account.LastName) {
        //         return Redirect("https://open.spotify.com/");
        //         // theoretically this should send the user to a new page that tells them they have the same spotify account as someone else
        //     }
        // }

        if (listener.SpotifyId == null) {
            listener.SpotifyId = currentSpotifyUser.Id;
            _listenerRepository.AddOrUpdate(listener);
        } else if (listener.SpotifyId != currentSpotifyUser.Id) {  
            HttpContext.SignOutAsync();
            return Redirect("https://open.spotify.com/");
            // AuthenticationProperties authentication = new AuthenticationProperties();
            // AuthenticationService.SignOutAsync(HttpContext, "Spotify", authentication);
        }

        return RedirectToAction("Index", "Listener");
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



