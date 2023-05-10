using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Services.Abstract;

public interface IMCMOpenAiService
{
    Task<string> GetTextResponseFromOpenAiFromUserInput(string UserInput, string Genre, PromptDTO promptDTO);
    Task<string> GetTextResponseFromOpenAiFromUserInputAuto(string userInput, PromptDTO promptDTO);
    Task<string> GetTitle(string titlePrompt, PromptDTO promptDTO);
}