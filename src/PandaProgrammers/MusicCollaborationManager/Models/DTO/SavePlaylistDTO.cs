namespace MusicCollaborationManager.Models.DTO
{
    public class SavePlaylistDTO
    {
        public List<string> NewTrackUris { get; set; }

        //-------------------
        public string NewPlaylistName { get; set; }
        public string NewPlaylistDescription { get; set; }
        public bool IsNewPlaylistPublic { get; set; }
    }
}
