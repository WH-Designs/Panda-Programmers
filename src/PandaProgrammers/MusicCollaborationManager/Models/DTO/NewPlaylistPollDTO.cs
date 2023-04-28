namespace MusicCollaborationManager.Models.DTO
{
    public class NewPlaylistPollDTO
    {
        public string TrackArtist { get; set; }
        public string TrackTitle { get; set; }
        public string TrackDuration { get; set; }

        //--Options (below)---
        public string YesOptionID { get; set; }
        public string NoOptionID { get; set; }
        public string TotalPollVotes { get; set; }
    }
}
