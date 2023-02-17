using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web.Auth;
using SpotifyAPI.Web;
using MusicCollaborationManager.Services.Concrete;


namespace MusicCollaborationManager.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly ISpotifyUserService _spotifyUserService;

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ISpotifyUserService spotifyService)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyUserService = spotifyService;
    }

    public IActionResult Index()
    {
        return View();
    }


    [HttpPost]
    public IActionResult SpotifyRedirect()
    {
        var loginRequest = new LoginRequest(new Uri("http://localhost:5191/auth/callback"), "ClientId", LoginRequest.ResponseType.Code)
        {
            Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserReadEmail }
        };

        var uri = loginRequest.ToUri();
        return View(uri);
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



