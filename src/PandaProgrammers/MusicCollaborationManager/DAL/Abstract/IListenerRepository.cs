using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.DAL.Abstract;

public interface IListenerRepository
{
    string GetListenerFullName(int userId);
    Listener FindListenerByAspId (string aspUserId);
}