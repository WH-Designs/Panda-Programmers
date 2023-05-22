using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.Services.Abstract;
using MusicCollaborationManager.Services.Concrete;
using SpotifyAPI.Web;
using System.Collections.Generic;
using MusicCollaborationManager.Models.DTO;
using static System.Net.Mime.MediaTypeNames;
using MusicCollaborationManager.DAL.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyAuthController : ControllerBase
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IListenerRepository _listenerRepository;
        private readonly SpotifyAuthService _spotifyService;

        public SpotifyAuthController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, SpotifyAuthService spotifyService, IListenerRepository listenerRepository)
        {
            _logger = logger;
            _userManager = userManager;
            _spotifyService = spotifyService;
            _listenerRepository = listenerRepository;
        }

        [HttpPost("search")]
        public async Task<SearchResultsDTO> Search([Bind("SearchQuery", "CheckedItems")] SearchDTO searchDTO)
        {

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            string query = searchDTO.SearchQuery;
            Dictionary<String, Boolean> types = searchDTO.CheckedItems;

            try
            {
                SearchResponse search = await _spotifyService.GetSearchResultsAsync(query, spotifyClient);
                SearchResultsDTO results = new SearchResultsDTO();

                results.Filter(searchDTO, search);

                return results;
            }
            catch (Exception)
            {
                SearchResultsDTO emptyResults = new SearchResultsDTO();
                return emptyResults;
            }
        }

        [HttpGet("authuser")]
        public async Task<PrivateUser> GetAuthUserAsync()
        {
            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            PrivateUser CurrentUser = await _spotifyService.GetAuthUserAsync(spotifyClient);
            return CurrentUser;
        }

        [HttpGet("authtopartists")]
        public async Task<List<FullArtist>> GetAuthUserAsyncTopArtists()
        {
            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            List<FullArtist> TopArtists = await _spotifyService.GetAuthTopArtistsAsync(spotifyClient);
            return TopArtists;
        }

        
        [HttpPost("savegeneratedplaylist")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<CreatedPlaylistDTO> SaveMCMGeneratedPlaylist([Bind("NewTrackUris, NewPlaylistName, NewPlaylistDescription")] SavePlaylistDTO NewPlaylistInfo)
        {

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            if (ModelState.IsValid) 
            {
                CreatedPlaylistDTO CreatedPlaylistInfo = new CreatedPlaylistDTO();
                CreatedPlaylistInfo.PlaylistId = null;

                PlaylistCreateRequest CreationRequest = new PlaylistCreateRequest(NewPlaylistInfo.NewPlaylistName) 
                { 
                    Public = NewPlaylistInfo.NewPlaylistIsVisible,
                    Description = NewPlaylistInfo.NewPlaylistDescription
                };

                UserProfileClient UserProfileClient = (UserProfileClient)SpotifyAuthService.GetUserProfileClientAsync(spotifyClient);
                PlaylistsClient PlaylistsClient = (PlaylistsClient)SpotifyAuthService.GetPlaylistsClientAsync(spotifyClient);

                FullPlaylist NewPlaylist = new FullPlaylist();
                try
                {
                    NewPlaylist = await SpotifyAuthService.CreateNewSpotifyPlaylistAsync(CreationRequest, UserProfileClient, PlaylistsClient, spotifyClient);
                }
                catch (Exception)
                {
                    CreatedPlaylistInfo.PlaylistId = null;
                    return CreatedPlaylistInfo;
                }

                try
                {
                    await _spotifyService.AddSongsToPlaylistAsync(NewPlaylist, NewPlaylistInfo.NewTrackUris, spotifyClient);

                    CreatedPlaylistInfo.PlaylistId = NewPlaylist.Id;
                    return CreatedPlaylistInfo;
                }
                catch (Exception)
                {
                    CreatedPlaylistInfo.PlaylistId = null;
                    return CreatedPlaylistInfo;
                }
            }
            return null;
        }

        //A return value of "false" indicates an error. "true" means successful.
        [HttpPut("changeplaylistcover")]
        public async Task<UploadCoverResultDTO> ChangePlaylistCoverImage([Bind("PlaylistId,PlaylistImgBaseString")] ChangePlaylistCoverDTO NewPlaylistInfo) 
        {

            string aspId = _userManager.GetUserId(User);
            Listener current_listener = _listenerRepository.FindListenerByAspId(aspId);
            SpotifyClient spotifyClient = await _spotifyService.GetSpotifyClientAsync(current_listener);

            UploadCoverResultDTO UploadCover = new UploadCoverResultDTO();
            UploadCover.CoverSaveSuccessful = false;
            if (NewPlaylistInfo.PlaylistImgBaseString == null) 
            {
                return UploadCover;
            }
            else if(NewPlaylistInfo.PlaylistImgBaseString.Length == 0)
            {
                return UploadCover;
            }
            else if(NewPlaylistInfo.PlaylistImgBaseString == "NO_PLAYLIST_COVER") 
            {
                UploadCover.CoverSaveSuccessful = true;
                return UploadCover;
            }

            UploadCover.CoverSaveSuccessful = await _spotifyService.ChangeCoverForPlaylist(NewPlaylistInfo.PlaylistId, NewPlaylistInfo.PlaylistImgBaseString, spotifyClient);
            return UploadCover;
        }
    }
}