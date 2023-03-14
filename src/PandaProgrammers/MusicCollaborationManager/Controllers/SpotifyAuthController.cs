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
        public async Task<SearchResultsDTO> Search([Bind("SearchQuery")] SearchDTO searchDTO)
        {
            string query = searchDTO.SearchQuery;

            try {
                SearchResponse search = await _spotifyService.GetSearchResultsAsync(query);
                SearchResultsDTO results = new SearchResultsDTO();

                results.AlbumsItems = search.Albums.Items;
                results.ArtistsItems = search.Artists.Items;
                results.PlaylistsItems = search.Playlists.Items;
                results.TracksItems = search.Tracks.Items;
                
                return results;
            } catch(Exception) {
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
        public async Task<bool> SaveMCMGeneratedPlaylist(List<string> newTrackUris)
        {
            bool NoErrorsWhileCreatingPlaylist = true;
            FullPlaylist NewPlaylist = new FullPlaylist();

            PlaylistCreateRequest CreationRequest = new PlaylistCreateRequest("MCM Playlist");
            UserProfileClient UserProfileClient = (UserProfileClient)SpotifyAuthService.GetUserProfileClientAsync();
            PlaylistsClient PlaylistsClient = (PlaylistsClient)SpotifyAuthService.GetPlaylistsClientAsync();

            try 
            {
                NewPlaylist = await SpotifyAuthService.CreateNewSpotifyPlaylistAsync(CreationRequest, UserProfileClient, PlaylistsClient);
            }
            catch (Exception) 
            {
                NoErrorsWhileCreatingPlaylist = false;
                return NoErrorsWhileCreatingPlaylist;
            }
            
            try 
            {
                await _spotifyService.AddSongsToPlaylistAsync(NewPlaylist, newTrackUris);
            }
            catch(Exception) 
            {
                NoErrorsWhileCreatingPlaylist = false;
                return NoErrorsWhileCreatingPlaylist;
            }
            
            return NoErrorsWhileCreatingPlaylist;
            
        }
    }
}