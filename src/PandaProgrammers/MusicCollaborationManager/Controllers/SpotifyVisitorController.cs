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
    public class SpotifyVisitorController : ControllerBase
    {
        private readonly ISpotifyVisitorService _spotifyService;

        public SpotifyVisitorController(ISpotifyVisitorService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("playlists")]
        public async Task<ActionResult<List<VisitorPlaylistDTO>>> GetVisitorPlaylists()
        {
           var Playlists = await _spotifyService.GetVisitorPlaylists();
           List<VisitorPlaylistDTO> PlaylistsToReturn = new List<VisitorPlaylistDTO>();

           int Index = 0;
           foreach (var item in Playlists.Playlists.Items)
           {
               VisitorPlaylistDTO SinglePlaylist = new VisitorPlaylistDTO();
               SinglePlaylist.SpotifyLinkToPlaylist = Playlists.Playlists.Items[Index]?.ExternalUrls["spotify"];

               var SpotifyLink = Playlists.Playlists.Items[Index]?.ExternalUrls["spotify"];
               var PlaylistImgObject = Playlists.Playlists.Items[0]?.Images;

                try {
                    
                    if (PlaylistImgObject != null)
                    {
                        SinglePlaylist.PlaylistImageURL = Playlists.Playlists.Items[Index].Images[0].Url;
                        SinglePlaylist.ImageHeight = Playlists.Playlists.Items[Index].Images[0].Height;
                        SinglePlaylist.ImageWidth = Playlists.Playlists.Items[Index].Images[0].Width;
                    }
                    
                    SinglePlaylist.PlaylistName = Playlists.Playlists.Items[Index].Name;

                } catch(Exception ex) {

                    Console.WriteLine(ex.Message + " INSIDE GETVISITORPLAYLIST");
                    SinglePlaylist.PlaylistImageURL = "https://i.scdn.co/image/ab67706f00000003163bab0613f6f5cbb87ea4da";
                    SinglePlaylist.ImageHeight = 0;
                    SinglePlaylist.ImageWidth = 0;
                    SinglePlaylist.PlaylistName = "teen angst";
                }

               PlaylistsToReturn.Add(SinglePlaylist);
               Index++;
           }

           return PlaylistsToReturn;
        }

        [HttpGet("topsongs")]
        public async Task<ActionResult<List<VisitorTrackDTO>>> GetTopTracks()
        {
            string ArtistId = "2CIMQHirSU0MQqyYHq0eOx"; //Artist: deadmau5
            string Region = "NA";

            var tracks = await _spotifyService.GetVisitorTracks(ArtistId, Region);
            List<VisitorTrackDTO> TracksToReturn = new List<VisitorTrackDTO>();

            int Index = 0;
            foreach (var song in tracks.Tracks)
            {
                VisitorTrackDTO Track = new VisitorTrackDTO();

                Track.Name = song.Name;
                Track.SpotifyTrackLinkURL = song.ExternalUrls["spotify"];

                var TrackImage = song.Album?.Images[0];

                if (TrackImage != null)
                {
                    Track.ImageURL = song.Album?.Images[0].Url;
                    Track.ImageWidth = song.Album.Images[0].Width;
                    Track.ImageHeight = song.Album.Images[0].Height;
                }
                
                TracksToReturn.Add(Track);

                if (Index > 4)
                    break;
                Index++;
            }

            return TracksToReturn;
        }
    }
}
