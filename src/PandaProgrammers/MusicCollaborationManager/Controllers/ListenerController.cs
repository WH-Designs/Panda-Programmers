using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.ViewModels;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Models.DTO;
using SpotifyAPI.Web;
using static NuGet.Packaging.PackagingConstants;

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
        public async Task<IActionResult> Index(UserDashboardViewModel vm)
        {
            string aspId = _userManager.GetUserId(User);

            Listener listener = new Listener();

            listener = _listenerRepository.FindListenerByAspId(aspId);

            vm.fullName = _listenerRepository.GetListenerFullName(listener.Id);

            vm.listener = listener;

            vm.aspUser = User;

            try
            {
                vm.TopTracks = await _spotifyService.GetAuthUserTopTracks();
                vm.FeatPlaylists = await _spotifyService.GetFeatPlaylists();
                vm.UserPlaylists = await _spotifyService.GetAuthPersonalPlaylists();
            }
            catch (NullReferenceException e) 
            {
                vm.TopTracks = new List<FullTrack>();
                vm.FeatPlaylists = new List<SimplePlaylist>();
                vm.UserPlaylists = new List<SimplePlaylist>();
            }
           

            return View(vm);
        }

        [Authorize]
        public IActionResult Profile(UserProfileViewModel vm)
        {
            string aspId = _userManager.GetUserId(User);
            Listener listener = new Listener();
            listener = _listenerRepository.FindListenerByAspId(aspId);
            vm.fullName = _listenerRepository.GetListenerFullName(listener.Id);
            vm.listener = listener;
            vm.aspUser = User;

            try
            {
                var holder = _spotifyService.GetAuthUser();

                vm.spotifyName = holder.Result.DisplayName;
                vm.accountType = holder.Result.Product;
                vm.country = holder.Result.Country;
                vm.followerCount = holder.Result.Followers.Total;
                if(holder.Result.Images.Count > 0)
                {
                    vm.profilePic = holder.Result.Images[0].Url;
                }
                else
                {
                    vm.profilePic = "https://t4america.org/wp-content/uploads/2016/10/Blank-User.jpg";
                }
            }
            catch (Exception ex)
            {
                vm.spotifyName = "Log in to see";
                vm.accountType = "Log in to see";
                vm.country = "Log in to see";
                vm.followerCount = 0;
                vm.profilePic = "https://t4america.org/wp-content/uploads/2016/10/Blank-User.jpg";
            }


            return View(vm);
        }
    }
}

