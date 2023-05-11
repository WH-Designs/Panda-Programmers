
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Concrete;

namespace MusicCollaborationManager.DAL.Concrete;

public class TutorialRepository : Repository<Tutorial>, ITutorialRepository
{
    public TutorialRepository(DbContext ctx) : base(ctx)
    {
    }

    public string GetTutorialLink(int tutorialID)
    {
        if(tutorialID == null)
        {
            return null;
        }

        try
        {
            var tutorial = new Tutorial();

            tutorial = _dbSet.First(t => t.Id.Equals(tutorialID));
            return tutorial.Link;
        }
        catch (InvalidOperationException)
        {
            return null;
        }
    }

}