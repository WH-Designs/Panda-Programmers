using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ListenerInfoController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IListenerRepository _listenerRepository;
        private readonly SpotifyAuthService _spotifyService;

        public ListenerInfoController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _spotifyService = spotifyService;
            _listenerRepository = listenerRepository;
        }

        [HttpGet("basicuserinfo/{username}")]
        public async Task<ListenerInfoDTO> GetBasicListenerInfo(string username)
        {
            ListenerInfoDTO InfoToReturn = new ListenerInfoDTO();

            IdentityUser? PossibleUser = await _userManager.FindByNameAsync(username);
            if (PossibleUser != null) {
                InfoToReturn.Username = PossibleUser.UserName;

                Listener MoreListenerInfo = _listenerRepository.FindListenerByAspId(PossibleUser.Id);
                InfoToReturn.FirstName = MoreListenerInfo.FirstName;
                InfoToReturn.LastName = MoreListenerInfo.LastName;
                return InfoToReturn;
            }

            return InfoToReturn;
        }
    }
}
