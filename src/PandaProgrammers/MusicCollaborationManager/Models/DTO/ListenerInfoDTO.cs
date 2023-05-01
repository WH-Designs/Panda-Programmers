using SpotifyAPI.Web;

namespace MusicCollaborationManager.Models.DTO
{
    public class ListenerInfoDTO
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Username { get; set; }
        public string? Theme { get; set; }
        public string? SpotifyId { get; set; }
        public bool? ConsentFlag { get; set; }
        public List<SimplePlaylist>? Playlists { get; set; }
    }
}
