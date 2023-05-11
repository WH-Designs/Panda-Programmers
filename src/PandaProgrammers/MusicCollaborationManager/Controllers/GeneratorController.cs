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
using OpenAI.Net.Models.Responses;

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
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> QuestionairePostAsync(QuestionViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserGenre = vm.genre;
                GeneratorUtilities utilities = new GeneratorUtilities();
                RecommendDTO recommendDTO = new RecommendDTO();
                recommendDTO = recommendDTO.convertToQuestionDTO(vm);
                RecommendationsResponse response = await _spotifyService.GetRecommendationsGenreBased(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;
                if (response.Tracks.Count == 0)
                {
                    //Calls questionairre dto method
                    recommendDTO = recommendDTO.convertToQuestionDTO(vm);
                    //Get seed artist
                    List<string> artistResult = await _spotifyService.SearchTopGenrePlaylistTrack(recommendDTO.genre[0]);
                    recommendDTO.seed.Add(artistResult[0]);

                    response = await _spotifyService.GetRecommendationsAsync(recommendDTO);
                    result = new List<SimpleTrack>();
                    result = response.Tracks;
                }
                string UserInputCoverImage = vm.coverImageInput;
                string UserInputDescription = vm.descriptionInput;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);
                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);
                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, UserGenre);

                if(generatorsViewModel.PlaylistCoverImageUrl != null) 
                {
                    string base64ImgString = await GeneratorsViewModel.ImageUrlToBase64(generatorsViewModel.PlaylistCoverImageUrl);
                    generatorsViewModel.PlaylistImgBase64 = base64ImgString;
                }
                else
                {
                    generatorsViewModel.PlaylistImgBase64 = "NO_PLAYLIST_COVER";
                }

                return View("GeneratedPlaylists", generatorsViewModel);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }

        }

        [Authorize]
        public IActionResult Mood(MoodViewModel vm)
        {
            try
            {
                return View("Mood", vm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
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
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        public IActionResult Time(TimeViewModel vm)
        {
            try
            {
                return View("Time", vm);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
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
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
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
                foreach (FullTrack track in seedTracks)
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
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        public async Task<IActionResult> TrackInputAsync()
        {
            try
            {
                TrackInputViewModel viewModel = new TrackInputViewModel();
                viewModel.seedTracks = await _spotifyService.GetTopTracksAsync();

                return View("TrackInput", viewModel);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TrackInputPostAsync(TrackInputViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserInputCoverImage = $"the song titled {vm.trackName} by {vm.artistName}";
                string UserInputDescription = $"the song titled {vm.trackName} by {vm.artistName}";
                RecommendDTO recommendDTO = new RecommendDTO();

                recommendDTO.seed.Add(vm.trackID);
                recommendDTO.limit = 20;
                RecommendationsResponse response = await _spotifyService.GetRecommendationsAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);
                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);
                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInputAuto(UserInputDescription);

                return View("GeneratedPlaylists", generatorsViewModel);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        public IActionResult TopArtist()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> TopArtistPostAsync(TopArtistViewModel vm)
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
                foreach (FullTrack track in seedTracks)
                {
                    seedIds.Add(track.Artists[0].Id);
                }
                List<string> shuffledArtists = generatorUtilities.shuffleTracks(seedIds);
                foreach (string artist in shuffledArtists)
                {
                    recommendDTO.artistSeed.Add(artist);
                }

                recommendDTO.limit = 20;
                RecommendationsResponse response = await _spotifyService.GetRecommendationsArtistBasedAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);
                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);
                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, null);

                return View("GeneratedPlaylists", generatorsViewModel);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        public async Task<IActionResult> RelatedArtists(RelatedArtistsViewModel vm)
        {
            try
            {
                var firstList = await _spotifyService.GetAuthTopArtistsAsync();
                var holder = await _spotifyService.GetAuthRelatedArtistsAsync(firstList);
                var seededVM = vm.SeedArtists(vm, holder);

                return View("RelatedArtists", seededVM);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> RelatedArtistsPostAsync(RelatedArtistsViewModel vm)
        {
            try
            {
                GeneratorsViewModel generatorsViewModel = new GeneratorsViewModel();
                string UserInputCoverImage = vm.coverImageInput;
                string UserInputDescription = vm.descriptionInput;
                string UserArtist = vm.Artist;

                RecommendDTO recommendDTO = new RecommendDTO();
                GeneratorUtilities generatorUtilities = new GeneratorUtilities();

                recommendDTO.artistSeed.Add(UserArtist);

                recommendDTO.limit = 20;
                RecommendationsResponse response = await _spotifyService.GetRecommendationsArtistBasedAsync(recommendDTO);
                List<SimpleTrack> result = new List<SimpleTrack>();
                result = response.Tracks;

                generatorsViewModel.fullResult = await _spotifyService.ConvertToFullTrackAsync(result);
                generatorsViewModel.PlaylistCoverImageUrl = _deepAiService.GetImageUrlFromApi(UserInputCoverImage);
                generatorsViewModel.PlaylistDescription = await _mcMOpenAiService.GetTextResponseFromOpenAiFromUserInput(UserInputDescription, null);

                return View("GeneratedPlaylists", generatorsViewModel);
            }
            catch (Exception)
            {
                ViewBag.Error = "Error Occured";
                return View("Index");
            }
        }

        [Authorize]
        public IActionResult GeneratedPlaylists()
        {
            return View();
        }
    }
}