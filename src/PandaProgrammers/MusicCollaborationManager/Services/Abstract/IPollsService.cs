using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Services.Abstract
{
    public interface IPollsService
    {
        public string? CreatePollForSpecificPlaylist(string spotifyPlaylistID);
        public IEnumerable<OptionInfoDTO> GetPollOptionsByPollID(string pollID);
        public void CreateVoteForTrack(string pollID, string optionID, string username);
        public VoteIdentifierInfoDTO GetSpecificUserVoteForAGivenPlaylist(string pollID, string username);
        public void RemoveVote(string voteID);
        public void RemovePoll(string pollID);
        public IEnumerable<PollDTO> GetAllPolls();
    }
}
