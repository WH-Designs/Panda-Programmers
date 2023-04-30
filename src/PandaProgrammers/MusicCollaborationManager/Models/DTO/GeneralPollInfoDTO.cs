namespace MusicCollaborationManager.Models.DTO
{
    public class GeneralPollInfoDTO
    {
        public string YesOptionID { get; set; } // GetPollOptionsInfo -- 
        public string NoOptionID { get; set; } // GetPollOptionsInfo
        public string TotalPollVotes { get; set; } // GetPollOptionsInfo

        //Number of people who voted "Yes" Votes & "No"
        public int YesVotes { get; set; } = 0; // GetPollOptionsInfo
        public int NoVotes { get; set; } = 0; // GetPollOptionsInfo

        //--------------------------------

        public string TrackArtist { get; set; } //GetPolledTrackInfo
        public string TrackTitle { get; set; } //GetPolledTrackInfo
        public string TrackDuration { get; set; } //GetPolledTrackInfo 


        //--------------------------------


        public string PlaylistFollowerCount { get; set; } //GetPlaylistFollowerCount

        //--------------------------------

        public bool? UserVotedYes { get; set; } = null; //GetUserVote

    }
}
