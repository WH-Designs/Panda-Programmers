using DeepAI;
using MusicCollaborationManager.Services.Abstract;

namespace MusicCollaborationManager.Services.Concrete;

public class DeepAiService : IDeepAIService
{
    public string DeepAiKey { get; set; }
    private static DeepAI_API _DeepApi { get; set; }

    public DeepAiService(string deepAiKey)
    {
        DeepAiKey = deepAiKey;

        _DeepApi = new DeepAI_API(apiKey: DeepAiKey);
    }

    public string GetImageUrlFromApi(string UserInput)
    {
        if (UserInput == null)
        {
            return null;
        }

        try
        {
            StandardApiResponse response = _DeepApi.callStandardApi("text2img", new
            {
                text = UserInput
            });

            return response.output_url;
        }
        catch (InvalidOperationException exception)
        {
            return null;
        }
    }
}