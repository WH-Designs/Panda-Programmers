using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Models;
using SpotifyAPI.Web;

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

        [HttpGet("basicuserinfo/me")]
        public ListenerInfoDTO GetListenerInfoForTheme()
        {
            try {
                string aspId = _userManager.GetUserId(User);
                Listener listener = new Listener();
                listener = _listenerRepository.FindListenerByAspId(aspId);
                
                ListenerInfoDTO InfoToReturn = new ListenerInfoDTO();

                InfoToReturn.Theme = listener.Theme;
                InfoToReturn.FirstName = listener.FirstName;
                InfoToReturn.LastName = listener.LastName;
                InfoToReturn.Username = "";
                
                if (listener.Theme == null) {
                    InfoToReturn.Theme = "classicpanda";
                }

                return InfoToReturn;

            } catch(NullReferenceException) {
                ListenerInfoDTO InfoToReturn = new ListenerInfoDTO();
                InfoToReturn.Theme = "classicpanda";
                return InfoToReturn;
            }
        }

        [HttpGet("basicuserinfo/getall")]
        public List<ListenerInfoDTO> GetAllListeners()
        {
            List<ListenerInfoDTO> returnList = new List<ListenerInfoDTO>();
            
            try {
                List<Listener> listenerList = _listenerRepository.GetAll().ToList();
                foreach(Listener listener in listenerList){
                    if (listener.SearchConsentFlag == true) {
                        ListenerInfoDTO infoTransferObject = new ListenerInfoDTO();
                        infoTransferObject.FirstName = listener.FirstName;
                        infoTransferObject.LastName = listener.LastName;
                        infoTransferObject.Username = listener.SpotifyUserName ?? "";
                        infoTransferObject.SpotifyId = listener.SpotifyId ?? "";
                        infoTransferObject.ConsentFlag = listener.SearchConsentFlag;
                        infoTransferObject.Theme = "";

                        returnList.Add(infoTransferObject);
                    } else if (listener.SearchConsentFlag == false){
                        continue;   
                    }
                }

                return returnList;

            } catch(Exception) {
                ListenerInfoDTO InfoToReturn = new ListenerInfoDTO();
                InfoToReturn.FirstName = "Error";
                returnList.Add(InfoToReturn);
                return returnList;
            }
        }
    }
}
