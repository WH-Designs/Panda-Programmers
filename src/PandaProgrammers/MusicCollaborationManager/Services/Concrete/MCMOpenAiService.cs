using Humanizer.Localisation;
using MusicCollaborationManager.Models.DTO;
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

    public async Task<string> GetTextResponseFromOpenAiFromUserInput(string UserInput, string Genre, PromptDTO promptDTO)
    {
        if (UserInput == null && Genre == null)
        {
            return null;
        }
        else if (Genre == null)
        {
            string inputOnly = string.Format(promptDTO.basicInput, UserInput);

            response = await _openAIService.TextCompletion.Get(inputOnly, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }
        else if (UserInput == null)
        {
            string defaultInput = string.Format(promptDTO.basicGenre, Genre);

            response = await _openAIService.TextCompletion.Get(defaultInput, o =>
            {
                o.N = 1;
                o.MaxTokens = 500;
            });
        }
        else
        {
            string InputPlus = string.Format(promptDTO.basicBoth, UserInput, Genre);
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

    public async Task<string> GetTitle(string titlePrompt, PromptDTO promptDTO)
    {
        if (titlePrompt == null)
        {
            return null;
        }
        else
        {
            string inputOnly = string.Format(promptDTO.title, titlePrompt);

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

    public async Task<string> GetTextResponseFromOpenAiFromUserInputAuto(string UserInput, PromptDTO promptDTO)
    {
        if (UserInput == null)
        {
            return null;
        }
        else
        {
            string inputOnly = string.Format(promptDTO.auto, UserInput);

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