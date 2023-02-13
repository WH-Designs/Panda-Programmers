using SpotifyAPI.Web;

namespace MusicCollaborationManager.Services.Abstract
{
    public interface ISpotifyService
    {
        Task<FeaturedPlaylistsResponse> GetVisitorPlaylists();
        Task<ArtistsTopTracksResponse> GetVisitorTracks(string artistId, string tracksFromRegion);
    }
}
