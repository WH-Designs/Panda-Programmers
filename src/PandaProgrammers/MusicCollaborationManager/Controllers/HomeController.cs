using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Models.DTO;


namespace MusicCollaborationManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly SpotifyClientBuilder _spotifyClientBuilder;
    private readonly SpotifyAuthService _spotifyService;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyClientBuilder spotifyClientBuilder, SpotifyAuthService spotifyService)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyClientBuilder = spotifyClientBuilder;
        _spotifyService = spotifyService;
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
        AuthorizedUserDTO authUser = new AuthorizedUserDTO();
        authUser.AuthClient = await _spotifyService.GetCallback(code);
        // authUser.Me = await authUser.AuthClient.UserProfile.Current();
        // authUser.Playlists = new List<SimplePlaylist>();
        // SpotifyAPI.Web.Paging<SimplePlaylist> playlist_page = await authUser.AuthClient.Playlists.GetUsers(authUser.Me.Id);
        // foreach (var item in playlist_page.Items){
        //     authUser.Playlists.Add(item);
        // }

        return View("UserPage");
    }

    public IActionResult UserPage()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



