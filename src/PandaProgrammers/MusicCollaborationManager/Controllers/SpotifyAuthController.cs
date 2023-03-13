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
        public async Task<PrivateUser> GetAuthUser()
        {
            PrivateUser CurrentUser = await _spotifyService.GetAuthUser();
            return CurrentUser;
        }    
        
        [HttpGet("authtoptracks")]
        public async Task<List<FullTrack>> GetAuthUserTopTracks()
        {
            List<FullTrack> TopTracks = await _spotifyService.GetAuthUserTopTracks();
            return TopTracks;
        }

        [HttpGet("authtopartists")]
        public async Task<List<FullArtist>> GetAuthUserTopArtists()
        {
            List<FullArtist> TopArtists = await _spotifyService.GetAuthTopArtists();
            return TopArtists;
        }

        [HttpGet("authplaylists")]
        public async Task<List<VisitorPlaylistDTO>> GetAuthFeatPlaylist()
        {
            var playlists = await _spotifyService.GetAuthFeatPlaylists();
            List<VisitorPlaylistDTO> PlaylistsToReturn = new List<VisitorPlaylistDTO>();


            foreach (var playlist in playlists.Playlists.Items)
            {
                VisitorPlaylistDTO IndividualPlaylist = new VisitorPlaylistDTO();
                IndividualPlaylist.SpotifyLinkToPlaylist = playlist.ExternalUrls["spotify"];
                IndividualPlaylist.PlaylistName = playlist.Name;

                if (playlist.Images != null)
                {
                    IndividualPlaylist.PlaylistImageURL = playlist.Images[0].Url;
                    IndividualPlaylist.ImageHeight = playlist.Images[0].Height;
                    IndividualPlaylist.ImageWidth = playlist.Images[0].Width;
                }
                PlaylistsToReturn.Add(IndividualPlaylist);
            }

            return PlaylistsToReturn;
        }

        [HttpGet("authpersonalplaylists")]
        public async Task<List<VisitorPlaylistDTO>> GetAuthPersonalPlaylist()
        {
            var personalPlaylists = await _spotifyService.GetAuthPersonalPlaylists();
            
            List<VisitorPlaylistDTO> PersonalPlaylistsToReturn = new List<VisitorPlaylistDTO>();

            foreach (var playlist in personalPlaylists)
            {
                VisitorPlaylistDTO IndividualPlaylist = new VisitorPlaylistDTO();
                IndividualPlaylist.SpotifyLinkToPlaylist = playlist.ExternalUrls["spotify"];
                IndividualPlaylist.PlaylistName = playlist.Name;

                if (playlist.Images != null)
                {
                    IndividualPlaylist.PlaylistImageURL = playlist.Images[0].Url;
                    IndividualPlaylist.ImageHeight = playlist.Images[0].Height;
                    IndividualPlaylist.ImageWidth = playlist.Images[0].Width;
                }
                PersonalPlaylistsToReturn.Add(IndividualPlaylist);
            }

            return PersonalPlaylistsToReturn;
        }

        [HttpPost("savegeneratedplaylist")]
        public async Task<bool> SaveMCMGeneratedPlaylist(List<string> newTrackUris)
        {
            bool NoErrorsWhileCreatingPlaylist = true;
            FullPlaylist NewPlaylist = new FullPlaylist();

            PlaylistCreateRequest CreationRequest = new PlaylistCreateRequest("MCM Playlist");
            UserProfileClient UserProfileClient = (UserProfileClient)SpotifyAuthService.GetUserProfileClient();
            PlaylistsClient PlaylistsClient = (PlaylistsClient)SpotifyAuthService.GetPlaylistsClient();

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