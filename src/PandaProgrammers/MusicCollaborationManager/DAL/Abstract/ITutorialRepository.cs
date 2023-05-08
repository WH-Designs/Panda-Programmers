
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;

namespace MusicCollaborationManager.DAL.Abstract;

public interface ITutorialRepository : IRepository<Tutorial>
{
    public string GetTutorialLink(int tutorialID);
}
