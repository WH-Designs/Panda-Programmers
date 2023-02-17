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

    public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyClientBuilder spotifyClientBuilder)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyClientBuilder = spotifyClientBuilder;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpGet]
    public async Task<UserProfileDTO> UserPage()
    {
        UserProfileDTO this_user = new UserProfileDTO();
        var spotify = await _spotifyClientBuilder.BuildClient();
        this_user.Me = await spotify.UserProfile.Current();
        return this_user;
    }


    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}



