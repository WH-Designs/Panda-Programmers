namespace MusicCollaborationManager.Models.DTO
{
    public class UserPlaylistDTO
    {
        public string LinkToPlaylist { get; set; }
        public string Name { get; set; }
        public string? ImageURL { get; set; } = null;
        public string Uri { get; set; }

    }
}
