using Microsoft.Net.Http.Headers;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Services.Abstract;

namespace MusicCollaborationManager.Services.Concrete
{
    public class YouTubeService : IYouTubeService
    {
        private readonly string _YTKey;
        public static readonly HttpClient _httpClient = new HttpClient();

        public YouTubeService(string yTKey)
        {
            _YTKey = yTKey;
        }

        public async Task<string> GetJsonStringFromEndpoint(string uri)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<MusicVideoDTO>> GetPopularMusicVideosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
