using SpotifyAPI.Web;

namespace MusicCollaborationManager.Models.DTO
{
    public class AuthorizedUserDTO
    {
        public PrivateUser Me { get; set; }
        public SpotifyClient AuthClient { get; set; }
        public List<SimplePlaylist> Playlists { get; set; }
    }
}