using SpotifyAPI.Web;

namespace MusicCollaborationManager.Services.Abstract
{
    public interface ISpotifyVisitorService
    {
        Task<FeaturedPlaylistsResponse> GetVisitorPlaylists();
        Task<ArtistsTopTracksResponse> GetVisitorTracks(string artistId, string tracksFromRegion);
    }
}
