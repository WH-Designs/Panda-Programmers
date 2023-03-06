using DeepAI;
using MusicCollaborationManager.Services.Abstract;

namespace MusicCollaborationManager.Services.Concrete;

public class DeepAiService : IDeepAiService
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
            StandardApiResponse response = _DeepApi.callStandardApi("stable-diffusion", new
            {
                grid_size = "1",
                text = UserInput,
                width = "300",
                height = "300"
            });

            return response.output_url;
        }
        catch (InvalidOperationException exception)
        {
            return null;
        }
    }
}