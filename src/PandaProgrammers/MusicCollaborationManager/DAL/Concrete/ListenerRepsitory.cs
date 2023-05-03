using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Concrete;

namespace MusicCollaborationManager.DAL.Concrete;

public class ListenerRepository : Repository<Listener>, IListenerRepository
{
    public ListenerRepository(DbContext ctx) : base(ctx)
    {
    }

    public string GetListenerFullName(int userId)
    {
        if (userId == 0)
        {
            return null;
        }

        try
        {
            string fullName = string.Empty;

            var listener = new Listener();

            listener = _dbSet.First(l => l.Id == userId);

            fullName = listener.FirstName + " " + listener.LastName;

            return fullName;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

    public Listener FindListenerByAspId(string aspUserId)
    {
        if (aspUserId == null)
        {
            return null;
        }

        try
        {
            var listener = new Listener();

            listener = _dbSet.First(l => l.AspnetIdentityId.Equals(aspUserId));
            return listener;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

     public void AddOrUpdateListener(Listener listener)
     {
         _dbSet.Update(listener);
     }
}