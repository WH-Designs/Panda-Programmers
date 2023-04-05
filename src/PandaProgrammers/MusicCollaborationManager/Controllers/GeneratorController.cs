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
using MusicCollaborationManager.Services.Abstract;
using MusicCollaborationManager.Utilities;
using Humanizer.Localisation;

namespace MusicCollaborationManager.Controllers
{
    public class GeneratorController : Controller
    {
        private readonly IListenerRepository _listenerRepository;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SpotifyAuthService _spotifyService;
        private readonly IDeepAiService _deepAiService;
        private readonly IMCMOpenAiService _mcMOpenAiService;

        public GeneratorController(IListenerRepository listenerRepository, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, SpotifyAuthService spotifyService, IDeepAiService deepAiService, IMCMOpenAiService mcMOpenAiService)
        {
            _listenerRepository = listenerRepository;
            _userManager = userManager;
            _signInManager = signInManager;
            _spotifyService = spotifyService;
            _deepAiService = deepAiService;
            _mcMOpenAiService = mcMOpenAiService;
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
                var holder = _spotifyService.GetSeedGenresAsync();
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
        public async Task<IActionResult> QuestionairePostAsync(QuestionViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserInputCoverImage = vm.coverImageInput;
                string UserInputDescription = vm.descriptionInput;
                string UserGenre = vm.genre;
                GeneratorUtilities utilities = new GeneratorUtilities();

                RecommendDTO recommendDTO = new RecommendDTO();
                //Calls questionairre dto method
                recommendDTO = recommendDTO.convertToQuestionDTO(vm);
                //Get seed artist
                List<string> artistResult = await _spotifyService.SearchTopGenrePlaylistTrack(recommendDTO.genre[0]);
                recommendDTO.seed.Add(artistResult[0]);

                RecommendationsResponse response = await _spotifyService.GetRecommendationsAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);

                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);

                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, UserGenre);

                return View("GeneratedPlaylists", generatorsViewModel);
            }
            catch (Exception e) 
            {
                //Error occurs when not logged into spotify
                return RedirectToAction("callforward", "Home");
            }

        }

        [Authorize]
        public IActionResult Mood(MoodViewModel vm)
        {
            try
            {
                return View("Mood", vm);
            }
            catch (Exception e)
            {
                return RedirectToAction("callforward", "Home");
            }

        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> MoodPostAsync(MoodViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserInputCoverImage = vm.coverImageInput;
                string UserInputDescription = vm.descriptionInput;

                RecommendDTO recommendDTO = new RecommendDTO();
                //Calls mood dto method
                recommendDTO = recommendDTO.convertToMoodDTO(vm);

                string UserGenre = recommendDTO.genre[0];

                List<string> trackResult = await _spotifyService.SearchTopGenrePlaylistTrack(recommendDTO.genre[0]);
                foreach (string track in trackResult)
                {
                    recommendDTO.seed.Add(track);
                }
                RecommendationsResponse response = await _spotifyService.GetRecommendationsAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);

                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);

                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, UserGenre);

                return View("GeneratedPlaylists", generatorsViewModel);

            }
            catch (Exception e)
            {
                //Error occurs when not logged into spotify
                return RedirectToAction("callforward", "Home");
            }
        }

        [Authorize]
        public IActionResult Time(TimeViewModel vm)
        {
            try
            {
                return View("Time", vm);
            }
            catch (Exception e)
            {
                return RedirectToAction("callforward", "Home");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TimePostAsync(TimeViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserInputCoverImage = vm.coverImageInput;
                string UserInputDescription = vm.descriptionInput;
                RecommendDTO recommendDTO = new RecommendDTO();
                GeneratorUtilities generatorUtilities = new GeneratorUtilities();

                //Sets time category
                vm.timeCategory = generatorUtilities.getTimeValue(DateTime.Now);
                //Calls time dto method                
                recommendDTO = recommendDTO.convertToTimeDTO(vm);

                string UserGenre = recommendDTO.genre[0];

                List<string> trackResult = await _spotifyService.SearchTopGenrePlaylistTrack(recommendDTO.genre[0]);
                foreach (string track in trackResult)
                {
                    recommendDTO.seed.Add(track);
                }

                RecommendationsResponse response = await _spotifyService.GetRecommendationsAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);

                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);

                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, UserGenre);

                return View("GeneratedPlaylists", generatorsViewModel);

            }
            catch (Exception e)
            {
                //Error occurs when not logged into spotify
                return RedirectToAction("callforward", "Home");
            }
        }

        [Authorize]
        public IActionResult TopTrack()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TopTracksPostAsync(TopTracksViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserInputCoverImage = vm.coverImageInput;
                string UserInputDescription = vm.descriptionInput;
                RecommendDTO recommendDTO = new RecommendDTO();
                GeneratorUtilities generatorUtilities = new GeneratorUtilities();
                List<string> seedIds = new List<string>();

                List<FullTrack> seedTracks = await _spotifyService.GetTopTracksAsync();
                if (seedTracks.Count <= 0)
                {
                    return RedirectToAction("callforward", "Home");
                }
                foreach(FullTrack track in seedTracks)
                {
                    seedIds.Add(track.Id);
                }
                List<string> shuffledTracks = generatorUtilities.shuffleTracks(seedIds);
                foreach (string track in shuffledTracks)
                {
                    recommendDTO.seed.Add(track);
                }

                recommendDTO.limit = 20;
                RecommendationsResponse response = await _spotifyService.GetRecommendationsAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);
                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);
                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, null);

                return View("GeneratedPlaylists", generatorsViewModel);

            }
            catch (Exception e)
            {
                //Error occurs when not logged into spotify
                return RedirectToAction("callforward", "Home");
            }
        }

        [Authorize]
        public IActionResult GeneratedPlaylists()
        {
            return View();
        }
    }
}