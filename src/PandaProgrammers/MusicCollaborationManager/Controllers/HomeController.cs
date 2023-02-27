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
using System.Web;


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
        await _spotifyService.GetCallback(code);
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



