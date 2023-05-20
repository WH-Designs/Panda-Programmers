using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Concrete;

namespace MusicCollaborationManager.DAL.Concrete;

public class SpotifyAuthorizationNeededRepository : Repository<SpotifyAuthorizationNeededListener>, ISpotifyAuthorizationNeededRepository
{
    private readonly DbContext _context;

    public SpotifyAuthorizationNeededRepository(DbContext ctx) : base(ctx)
    {
        _context = ctx;
    }

    public void AddOrUpdateSpotifyAuthListener(SpotifyAuthorizationNeededListener listener) {
        _context.Update(listener);
        _context.SaveChanges();
    }
}