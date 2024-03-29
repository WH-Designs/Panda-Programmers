namespace MusicCollaborationManager.Models.DTO
{
    public class FullPlaylistDTO
    {
        public string LinkToPlaylist { get; set; }
        public string Name { get; set; }
        public string Owner { get; set; }
        public string? ImageURL { get; set; } = null;
        public string Uri { get; set; }
        public string Desc { get; set; }
        public string PlaylistId { get; set; }
        public List<UserTrackDTO> Tracks { get; set; }
        public int ListenerId { get; set; }
    }
}