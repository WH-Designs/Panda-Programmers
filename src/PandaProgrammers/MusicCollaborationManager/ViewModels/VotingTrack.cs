namespace MusicCollaborationManager.ViewModels
{
    public class VotingTrack
    {
        public string? CurUserVoteOption { get; set; } = null; //'null' indicates the user has not voted.
        public string Artist { get; set; }
        public string Name { get; set; }
        public string Duration { get; set; }
        public int TotalVotes { get; set; } = 0;
    }
}
