namespace MusicCollaborationManager.Models.DTO
{
    public class SavePlaylistDTO
    {
        public List<string> NewTrackUris { get; set; }
        public string NewPlaylistName { get; set; }
        public bool NewPlaylistIsVisible { get; set; }
    }
}
