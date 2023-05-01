using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.DAL.Concrete
{
    public class PlaylistPollRepository : Repository<Poll>, IPlaylistPollRepository
    {
        public PlaylistPollRepository(DbContext ctx) : base(ctx)
        {
        }

        public Poll GetPollDetailsBySpotifyPlaylistID(string curSpotifyPlaylistID)
        {
            return _dbSet.SingleOrDefault(poll => poll.SpotifyPlaylistId == curSpotifyPlaylistID);
        }

    }
}
