using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Services.Abstract
{
    public interface IPollsService
    {
        public Task<string?> CreatePollForSpecificPlaylist(string spotifyPlaylistID);
        public Task<IEnumerable<OptionInfoDTO>> GetPollOptionsByPollID(string pollID);
        public Task CreateVoteForTrack(string pollID, string optionID, string username);
        public Task<VoteIdentifierInfoDTO> GetSpecificUserVoteForAGivenPlaylist(string pollID, string username);
        public Task RemoveVote(string voteID);
        public Task RemovePoll(string pollID);
        public Task<IEnumerable<PollDTO>> GetAllPolls();
    }
}
