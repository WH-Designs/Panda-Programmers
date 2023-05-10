
using System.Configuration;
using Microsoft.EntityFrameworkCore;
using MusicCollaborationManager.DAL.Abstract;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.DAL.Concrete;
using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.DAL.Concrete;

public class PromptRepository : Repository<Prompt>, IPromptRepository
{
    public PromptRepository(DbContext ctx) : base(ctx)
    {
    }

    public PromptDTO GetPromptDTO()
    {
        PromptDTO promptDTO = new PromptDTO();
        var holder = GetAll();
        var holderList = holder.ToList();
        promptDTO.basicInput = holderList[0].Prompt1;
        promptDTO.basicGenre = holderList[1].Prompt1;
        promptDTO.basicBoth = holderList[2].Prompt1;
        promptDTO.title = holderList[3].Prompt1;
        promptDTO.auto = holderList[4].Prompt1;

        return promptDTO;
    }
}

