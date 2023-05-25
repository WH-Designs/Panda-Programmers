using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.DAL.Abstract;

public interface IPromptRepository : IRepository<Prompt>
{
    public PromptDTO GetPromptDTO();
}
