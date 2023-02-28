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
            FullPlaylist NewPlaylist = new FullPlaylist();
            NewPlaylist = await _spotifyService.CreateNewSpotifyPlaylist();

            return await _spotifyService.AddSongsToPlaylist(NewPlaylist, newTrackUris);
        }
    }
}