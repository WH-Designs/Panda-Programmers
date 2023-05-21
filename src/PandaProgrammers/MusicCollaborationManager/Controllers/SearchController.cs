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

public class SearchController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly UserManager<IdentityUser> _userManager;
    private readonly IListenerRepository _listenerRepository;
    private readonly SpotifyAuthService _spotifyService;

    public SearchController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository)
    {
        _logger = logger;
        _userManager = userManager;
        _spotifyService = spotifyService;
        _listenerRepository = listenerRepository;

    }

    [Authorize]
    public async Task<IActionResult> Search() 
    {
        try {
            string aspId = _userManager.GetUserId(User);
            Listener listener = new Listener();
            listener = _listenerRepository.FindListenerByAspId(aspId);
            string name = listener.FirstName;
            
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(listener);
            await _spotifyService.GetAuthUserAsync(spotifyClient);

            return View("Search");

        } catch (Exception e) {
            Console.WriteLine(e.Message);
            TempData["Error"] = "Error Occured";
            return RedirectToAction("Index", "Home");
        }
    }
    
    [Authorize]
    public async Task<IActionResult> PlaylistsDisplay(string spotifyID)
    {
        try {
            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            List<SimplePlaylist> usersPlaylists = await _spotifyService.GetUserPlaylists(spotifyID, spotifyClient);
            return View("PlaylistsDisplay", usersPlaylists);

        } catch (Exception e) {
            Console.WriteLine(e.Message);
            TempData["Error"] = "Error Occured";
            return RedirectToAction("Index", "Home");
        }
    }

    [Authorize]
    public async Task<IActionResult> Like(string playlistID) 
    {
        try{

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            await _spotifyService.LikePlaylist(playlistID, spotifyClient);
            return Redirect("/listener");
        } catch(Exception e) {
            Console.WriteLine(e.Message);
            TempData["Error"] = "Error Occured";
            return RedirectToAction("Index", "Home");
        }
    }
}