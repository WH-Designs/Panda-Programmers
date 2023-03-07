using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.DAL.Abstract;

public interface IListenerRepository: IRepository<Listener>
{
    string GetListenerFullName(int userId);
    Listener FindListenerByAspId (string aspUserId);
}