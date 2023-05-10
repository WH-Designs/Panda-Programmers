using MusicCollaborationManager.Services.Abstract;
using OpenAI.Net;
using OpenAI.Net.Models.OperationResult;
using OpenAI.Net.Models.Requests;
using OpenAI.Net.Models.Responses;
using OpenAI.Net.Models.Responses.Common;

namespace MusicCollaborationManager.Services.Concrete;

class MCMOpenAiService : IMCMOpenAiService
{
    private static IOpenAIService _openAIService { get; set; }
    public string ErrorMessage { get; set; }

    public OpenAIHttpOperationResult<TextCompletionResponse, OpenAI.Net.Models.Responses.Common.ErrorResponse> response;

    public MCMOpenAiService(IOpenAIService OpenAiService)
    {
        _openAIService = OpenAiService;
    }

    public async Task<string> GetTextResponseFromOpenAiFromUserInput(string UserInput, string Genre)
    {
        if (UserInput == null && Genre == null)
        {
            return null;
        }
        else if (Genre == null)
        {
            string inputOnly = $"Using these words {UserInput}, give a detailed description of a music playlist and don't mention any specific artists.";

            response = await _openAIService.TextCompletion.Get(inputOnly, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }
        else if (UserInput == null)
        {
            string defaultInput = $"Give me a detailed paragraph describing an {Genre} music playlist and don't mention any specific artists.";

            response = await _openAIService.TextCompletion.Get(defaultInput, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }
        else
        {
            string InputPlus = $"Using these words {UserInput}, give a detailed description of a {Genre} music playlist and don't mention any specific artists.";

            response = await _openAIService.TextCompletion.Get(InputPlus, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }

        if (response.IsSuccess)
        {
            string result = String.Join(" ", response.Result.Choices.Select(i => i.Text).ToList());

            return result;
        }
        else
        {
            return null;
        }
    }

    public async Task<string> GetTitle(string titlePrompt)
    {
        if (titlePrompt == null)
        {
            return null;
        }
        else
        {
            string inputOnly = $"Using these description word or words {titlePrompt}, create a short title for a playlist. Do not return the title in quotes.";

            response = await _openAIService.TextCompletion.Get(inputOnly, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }

        if (response.IsSuccess)
        {
            string result = String.Join(" ", response.Result.Choices.Select(i => i.Text).ToList());
            result = result.Replace('"', ' ').Trim();
            return result;
        }
        else
        {
            return null;
        }
    }

    public async Task<string> GetTextResponseFromOpenAiFromUserInputAuto(string UserInput)
    {
        if (UserInput == null)
        {
            return null;
        }
        else
        {
            string inputOnly = $"Using these words {UserInput}, give a general description of the type of music that could be found in a playlist of similar songs and don't mention any specific artists and don't describe specific songs in the playlist.";

            response = await _openAIService.TextCompletion.Get(inputOnly, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }

        if (response.IsSuccess)
        {
            string result = String.Join(" ", response.Result.Choices.Select(i => i.Text).ToList());

            return result;
        }
        else
        {
            return null;
        }
    }
}