using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Services.Abstract;
using MusicCollaborationManager.Services.Concrete;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PlaylistPollsController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IListenerRepository _listenerRepository;
        private readonly SpotifyAuthService _spotifyService;
        private readonly IPollsService _pollsService;

        public PlaylistPollsController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository, 
            IPollsService pollsService)
        {
            _logger = logger;
            _userManager = userManager;
            _spotifyService = spotifyService;
            _listenerRepository = listenerRepository;
            _pollsService = pollsService;
        }

        // [HttpPost("createpoll/{playlistid}/{trackuri}")]
        // public async Task CreateNewPoll(string playlistid, string trackuri)
        // {
        //     _listenerRepository
        // }


        //Probably safer to check the 'spotify_playlist_id' a potentially existing poll and load the existing track being polled on as part of the view model.
    }
}
