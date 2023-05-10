namespace MusicCollaborationManager.Services.Abstract;

public interface IMCMOpenAiService
{
    Task<string> GetTextResponseFromOpenAiFromUserInput(string UserInput, string Genre);
    Task<string> GetTextResponseFromOpenAiFromUserInputAuto(string userInput);
    Task<string> GetTitle(string titlePrompt);
}