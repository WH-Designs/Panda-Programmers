using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;

namespace MusicCollaborationManager.DAL.Concrete
{
    public class CommentRepository : Repository<Comment>, ICommentRepository
    {
        public CommentRepository(DbContext ctx) : base(ctx)
        {
        }

        public List<Comment> GetAllOfCommentsForPlaylist(string serviceId)
        {
            List<Comment> comments = new List<Comment>();

            if (serviceId == null)
            {
                return null;
            }

            try
            {
                comments = _dbSet.Where(c => c.SpotifyId.Equals(serviceId)).ToList();
                return comments;
            }
            catch (InvalidOperationException ex)
            {
                return null;
            }
        }
    }
}
