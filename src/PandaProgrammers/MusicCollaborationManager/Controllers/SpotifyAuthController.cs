using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MusicCollaborationManager.Services.Abstract;
using MusicCollaborationManager.Services.Concrete;
using SpotifyAPI.Web;
using System.Collections.Generic;
using MusicCollaborationManager.Models.DTO;
using static System.Net.Mime.MediaTypeNames;

namespace MusicCollaborationManager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpotifyAuthController : ControllerBase
    {
        private readonly SpotifyAuthService _spotifyService;

        public SpotifyAuthController(SpotifyAuthService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpPost("search")]
        public async Task<SearchResultsDTO> Search([Bind("SearchQuery", "CheckedItems")] SearchDTO searchDTO)
        {
            string query = searchDTO.SearchQuery;
            Dictionary<String, Boolean> types = searchDTO.CheckedItems;

            try
            {
                SearchResponse search = await _spotifyService.GetSearchResultsAsync(query);
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
            PrivateUser CurrentUser = await _spotifyService.GetAuthUserAsync();
            return CurrentUser;
        }

        [HttpGet("authtopartists")]
        public async Task<List<FullArtist>> GetAuthUserAsyncTopArtists()
        {
            List<FullArtist> TopArtists = await _spotifyService.GetAuthTopArtistsAsync();
            return TopArtists;
        }

        
        [HttpPost("savegeneratedplaylist")]
        [ProducesResponseType(StatusCodes.Status200OK)] 
        public async Task<CreatedPlaylistDTO> SaveMCMGeneratedPlaylist([Bind("NewTrackUris, NewPlaylistName, NewPlaylistDescription, IsNewPlaylistPublic")] SavePlaylistDTO NewPlaylistInfo)
        {
            FullPlaylist NewPlaylist = new FullPlaylist();
            CreatedPlaylistDTO CreatedPlaylistInfo = new CreatedPlaylistDTO();
            CreatedPlaylistInfo.PlaylistId = null;

            //PlaylistCreateRequest CreationRequest = new PlaylistCreateRequest("MCM Playlist");
            PlaylistCreateRequest CreationRequest = new PlaylistCreateRequest(NewPlaylistInfo.NewPlaylistName);
            CreationRequest.Description = NewPlaylistInfo.NewPlaylistDescription;
            CreationRequest.Public = NewPlaylistInfo.IsNewPlaylistPublic;

            UserProfileClient UserProfileClient = (UserProfileClient)SpotifyAuthService.GetUserProfileClientAsync();
            PlaylistsClient PlaylistsClient = (PlaylistsClient)SpotifyAuthService.GetPlaylistsClientAsync();

            try
            {
                NewPlaylist = await SpotifyAuthService.CreateNewSpotifyPlaylistAsync(CreationRequest, UserProfileClient, PlaylistsClient);
            }
            catch (Exception)
            {
                CreatedPlaylistInfo.PlaylistId = null;
                return CreatedPlaylistInfo;
            }

            try
            {
                await _spotifyService.AddSongsToPlaylistAsync(NewPlaylist, NewPlaylistInfo.NewTrackUris);



                CreatedPlaylistInfo.PlaylistId = NewPlaylist.Id;
                return CreatedPlaylistInfo;
            }
            catch (Exception)
            {
                CreatedPlaylistInfo.PlaylistId = null;
                return CreatedPlaylistInfo;
            }
        }

        [HttpPut("changeplaylistcover")]
        public async Task<bool> ChangePlaylistCoverImage([Bind("PlaylistId,PlaylistImgBaseString")] ChangePlaylistCoverDTO NewPlaylistInfo) 
        {
            return await _spotifyService.ChangeCoverForPlaylist(NewPlaylistInfo.PlaylistId, NewPlaylistInfo.PlaylistId);
        }
    }
}