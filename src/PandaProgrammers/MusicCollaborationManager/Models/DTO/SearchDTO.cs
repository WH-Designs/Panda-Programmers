using Microsoft.AspNetCore.Authentication;
using MusicCollaborationManager.Models;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.Models.DTO
{
    public class SearchDTO {
        public string SearchQuery { get; set; }
    }

    public class SearchResultsDTO {
        public List<SimpleAlbum> AlbumsItems { get; set; }
        public List<FullArtist> ArtistsItems { get; set; }
        public List<SimplePlaylist> PlaylistsItems { get; set; }
        public List<FullTrack> TracksItems { get; set; }
    }
}