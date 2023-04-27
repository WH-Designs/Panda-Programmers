using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.DAL.Abstract
{
    public interface ICommentRepository : IRepository<Comment>
    {
        List<Comment> GetAllOfCommentsForPlaylist(string serviceId);
    }
}
