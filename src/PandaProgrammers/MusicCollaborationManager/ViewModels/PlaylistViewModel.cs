using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.ViewModels
{
    public class PlaylistViewModel
    {
        public PlaylistViewModel() 
        {
        }

        public string? CurUserVoteOption { get; set; } = null; //'null' indicates the user has not voted.
        public IEnumerable<OptionInfoDTO> PlaylistVoteOptions { get; set; } = null; //'null' indicates there is no current poll.
        public int NumPlaylistFollowers { get; set; }
        public FullPlaylistDTO PlaylistToDisplay = new FullPlaylistDTO();
        public VotingTrack TrackVotedOn { get; set; } = null; //'null' indicates there is no current poll.

    }
}
