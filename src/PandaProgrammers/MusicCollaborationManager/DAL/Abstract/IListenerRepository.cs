using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;

namespace MusicCollaborationManager.DAL.Abstract;

public interface IListenerRepository : IRepository<Listener>
{
    string GetListenerFullName(int userId);
    Listener FindListenerByAspId(string aspUserId);
    void AddOrUpdateListener(Listener listener);
}
