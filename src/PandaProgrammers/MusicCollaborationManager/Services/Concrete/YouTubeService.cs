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
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Headers =
                    {
                        { HeaderNames.Accept, "application/json" }
                    }
            };
            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return responseText;
            }
            else
            {
                // What to do if failure? 401? Should throw and catch specific exceptions that explain what happened.
                return null;
            }

        }


        public async Task<IEnumerable<MusicVideoDTO>> GetPopularMusicVideosAsync()
        {
            string source = "https://youtube.googleapis.com/youtube/v3/videos?part=snippet%2Cstatistics&chart=mostPopular&regionCode=US&videoCategoryId=10&key=" + _YTKey;
            string response = await GetJsonStringFromEndpoint(source);

            IEnumerable<MusicVideoDTO> MusicVideos = MusicVideoDTO.FromJSON(response);


            return MusicVideos;
        }
    }
}
