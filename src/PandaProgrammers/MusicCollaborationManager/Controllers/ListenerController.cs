using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.ViewModels;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Controllers
{
    public class ListenerController : Controller
    {
        private readonly IListenerRepository _listenerRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SpotifyAuthService _spotifyService;

        public ListenerController(IListenerRepository listenerRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SpotifyAuthService spotifyService)
        {
            _listenerRepository = listenerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _spotifyService = spotifyService;
        }

        [Authorize]
        public IActionResult Index(UserDashboardViewModel vm)
        {
            
            //fix this to check if the cookie is something that exists

            string aspId = _userManager.GetUserId(User);

            Listener listener = new Listener();

            listener = _listenerRepository.FindListenerByAspId(aspId);

            vm.fullName = _listenerRepository.GetListenerFullName(listener.Id);

            vm.listener = listener;

            vm.aspUser = User;

            return View(vm);
        }
    }
}
