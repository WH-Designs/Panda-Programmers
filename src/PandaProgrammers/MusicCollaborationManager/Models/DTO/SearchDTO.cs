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

    public class ArtistDTO {
        public string name { get; set; }
        public string externalUrl { get; set; }
        public int popularity { get; set; }
        public string type { get; set; }
        public string imageUrl { get; set; }

    }

    public class AlbumDTO {
        public string name { get; set; }
        public string externalUrl { get; set; }
        public string albumType { get; set; }   
        public string imageUrl { get; set; }
        public string type { get; set; }
        public ArtistDTO artist { get; set; }

    }

    public class PlaylistDTO {
        public string name { get; set; }
        public string owner { get; set; }
        public string externalUrl { get; set; }
        public string imageUrl { get; set; }
        public string type { get; set; }
    }

    public class TrackDTO {
        public AlbumDTO album { get; set; }
        public ArtistDTO artist { get; set; }
        public string name { get; set; }
        public string externalUrl { get; set; }
        public int popularity { get; set; }
    }
}