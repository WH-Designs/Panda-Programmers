namespace MusicCollaborationManager.Models.DTO
{
    public class GeneralPollInfoDTO
    {
        public string TrackArtist { get; set; }
        public string TrackTitle { get; set; }
        public string TrackDuration { get; set; }

        //--Options (below)---
        public string YesOptionID { get; set; }
        public string NoOptionID { get; set; }
        public string TotalPollVotes { get; set; }
        public string PlaylistFollowerCount { get; set; }

        //Num of yes & no. Meant only for "createvote" endpoint. (below)------------

        public int YesVotes { get; set; } = 0;
        public int NoVotes { get; set; } = 0;

        public bool? UserVotedYes { get; set; } = null;

    }
}
