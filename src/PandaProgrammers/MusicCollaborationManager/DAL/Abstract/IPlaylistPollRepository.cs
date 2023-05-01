using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.DAL.Abstract
{
    public interface IPlaylistPollRepository : IRepository<Poll>
    {
        public Poll GetPollDetailsBySpotifyPlaylistID(string curSpotifyPlaylistID);
    }
}
