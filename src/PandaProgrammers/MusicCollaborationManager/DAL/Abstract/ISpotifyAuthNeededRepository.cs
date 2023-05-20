using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;

namespace MusicCollaborationManager.DAL.Abstract;

public interface ISpotifyAuthNeededRepository : IRepository<SpotifyAuthorizationNeededListener>
{
    void AddOrUpdateSpotifyAuthListener(SpotifyAuthorizationNeededListener listener);
}