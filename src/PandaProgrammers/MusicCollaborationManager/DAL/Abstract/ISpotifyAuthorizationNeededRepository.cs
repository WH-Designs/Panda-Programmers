using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;

namespace MusicCollaborationManager.DAL.Abstract;

public interface ISpotifyAuthorizationNeededRepository : IRepository<SpotifyAuthorizationNeededListener>
{
    void AddOrUpdateSpotifyAuthListener(SpotifyAuthorizationNeededListener listener);
}