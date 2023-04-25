using Microsoft.AspNetCore.Authentication;
using MusicCollaborationManager.Models;
using SpotifyAPI.Web;

namespace MusicCollaborationManager.Models.DTO
{
    public class SearchDTO {
        public string SearchQuery { get; set; }
        public Dictionary<String, Boolean> CheckedItems { get; set; }
    }

    public class SearchResultsDTO {
        public List<SimpleAlbum> AlbumsItems { get; set; }
        public List<FullArtist> ArtistsItems { get; set; }
        public List<SimplePlaylist> PlaylistsItems { get; set; }
        public List<FullTrack> TracksItems { get; set; }

        public void Filter(SearchDTO searchObject, SearchResponse searchResponse) {
            int count = 0;

            foreach (KeyValuePair<string, bool> item in searchObject.CheckedItems) {
                if (item.Value == true) {
                    if (item.Key == "All") {
                        this.ArtistsItems = searchResponse.Artists.Items;
                        this.PlaylistsItems = searchResponse.Playlists.Items;
                        this.TracksItems = searchResponse.Tracks.Items;
                        this.AlbumsItems = searchResponse.Albums.Items;

                        break;
                    } else if (item.Key == "Artists") {
                        this.ArtistsItems = searchResponse.Artists.Items;
                    } else if (item.Key == "Playlists") {
                        this.PlaylistsItems = searchResponse.Playlists.Items;
                    } else if (item.Key == "Tracks") {
                        this.TracksItems = searchResponse.Tracks.Items;
                    } else if (item.Key == "Albums") {
                        this.AlbumsItems = searchResponse.Albums.Items;
                    }
                } else if (item.Value == false) {
                    count++;
                }
            }

            if (count == 5) {
                this.ArtistsItems = searchResponse.Artists.Items;
                this.PlaylistsItems = searchResponse.Playlists.Items;
                this.TracksItems = searchResponse.Tracks.Items;
                this.AlbumsItems = searchResponse.Albums.Items;
            }

        }
    }
}