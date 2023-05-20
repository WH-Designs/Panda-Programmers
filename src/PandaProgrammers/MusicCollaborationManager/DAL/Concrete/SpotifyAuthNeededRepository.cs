using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Concrete;

namespace MusicCollaborationManager.DAL.Concrete;

public class SpotifyAuthNeededRepository : Repository<SpotifyAuthorizationNeededListener>, ISpotifyAuthNeededRepository
{
    public SpotifyAuthNeededRepository(DbContext ctx) : base(ctx)
    {}

    public void AddOrUpdateSpotifyAuthListener(SpotifyAuthorizationNeededListener listener) {
        _dbSet.Update(listener);
    }
}