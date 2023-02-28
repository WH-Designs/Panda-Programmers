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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicCollaborationManager.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly IListenerRepository _listenerRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SpotifyAuthService _spotifyService;

        public GeneratorController(IListenerRepository listenerRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SpotifyAuthService spotifyService)
        {
            _listenerRepository = listenerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _spotifyService = spotifyService;
        }

        [Authorize]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult Questionaire(QuestionViewModel vm)
        {
            try
            {
                var holder = _spotifyService.GetSeedGenres();
                var seededVM = vm.SeedGenres(vm, holder);

                return View("Questionaire", seededVM);
            }
            catch (Exception e)
            {
                return RedirectToAction("callforward", "Home");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> QuestionairePost(QuestionViewModel vm)
        {
            try
            {
                RecommendDTO recommendDTO = new RecommendDTO();
                recommendDTO = recommendDTO.convertToDTO(vm);
                var response = _spotifyService.GetRecommendations(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Result.Tracks;

                List<FullTrack> fullResult = new List<FullTrack>();
                fullResult = await _spotifyService.ConvertToFullTrack(result);
                return View("GeneratedPlaylists", fullResult);
            }
            catch (Exception e) 
            {
                return RedirectToAction("callforward", "Home");
            }

        }

        [Authorize]
        public IActionResult Mood()
        {
            return View();
        }

        [Authorize]
        public IActionResult Time()
        {
            return View();
        }

        [Authorize]
        public IActionResult GeneratedPlaylists()
        {
            return View();
        }
    }
}