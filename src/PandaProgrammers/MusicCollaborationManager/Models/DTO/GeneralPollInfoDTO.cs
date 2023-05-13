namespace MusicCollaborationManager.Models.DTO
{
    //These properties are now listed in the order in which it is safe to retrieve them (using the methods defined below) inthis info from within the API controller
    public class GeneralPollInfoDTO
    {
        //1) GetPollOptionsInfo -- 
        public string YesOptionID { get; set; }
        public string NoOptionID { get; set; }
        public string TotalPollVotes { get; set; }

        //Number of people who voted "Yes" & "No"
        public int YesVotes { get; set; } = 0;
        public int NoVotes { get; set; } = 0;

        //--------------------------------

        //2)  //GetPolledTrackInfo
        public string TrackArtist { get; set; }
        public string TrackTitle { get; set; }
        public string TrackDuration { get; set; }


        //--------------------------------

        //3) //GetPlaylistFollowerCount
        public string PlaylistFollowerCount { get; set; }

        //--------------------------------

        //4) GetUserVote
        public bool? UserVotedYes { get; set; } = null;

    }
}
