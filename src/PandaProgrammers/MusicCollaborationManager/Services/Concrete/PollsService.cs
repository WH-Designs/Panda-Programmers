using Microsoft.Net.Http.Headers;
using Newtonsoft.Json;
using OpenAI;
using System.Text;

using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Services.Abstract;

namespace MusicCollaborationManager.Services.Concrete
{
    public class PollsService:IPollsService
    {
        public string Name { get; set; }
        private string ApiKey { get; set; }
        private string BaseSource { get; set; }
        public static readonly HttpClient _httpClient = new HttpClient();

        public PollsService(string key) 
        {
            ApiKey = key;
            BaseSource = "https://api.pollsapi.com/v1";
        }

        public async Task<string> GetJsonStringFromEndpointPost(string bearerToken, string uri, string request)
        {
            StringContent bodyRequest = new StringContent(request, Encoding.UTF8, "application/json");
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Post, uri)
            {
                Headers =
                    {
                        { HeaderNames.Accept, "application/json" },
                    },
                Content = bodyRequest
            };
            httpRequestMessage.Headers.Add("api-key", bearerToken);

            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return responseText;
            }
            else
            {
                Console.WriteLine("An error occurred. HTTP status code was: {0}", response.StatusCode);

                if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return response.StatusCode.ToString();
                }
                return null;

            }
        }

        public async Task<string> GetJsonStringFromEndpointGet(string bearerToken, string uri)
        {
            HttpRequestMessage httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, uri)
            {
                Headers =
                    {
                        { HeaderNames.Accept, "application/json" },

                    }
            };
            httpRequestMessage.Headers.Add("api-key", bearerToken);


            HttpResponseMessage response = _httpClient.Send(httpRequestMessage);
            if (response.IsSuccessStatusCode)
            {
                string responseText = response.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                return responseText;
            }
            else
            {
                throw new HttpRequestException();
            }
        }

        public async Task<string?> CreatePollForSpecificPlaylist(string spotifyPlaylistID, string trackName)
        {
            string source = BaseSource + "/create/poll";
            string bodyRequest = "{\r\n    \"question\": \"Add '" + trackName + "' to playlist?\",\r\n    \"identifier\": \"" + spotifyPlaylistID + "\",\r\n    \"options\": [\r\n        {\r\n            \"text\": \"Yes\"\r\n        },\r\n        {\r\n            \"text\": \"No\"\r\n        }\r\n    ]\r\n}";

            string response = await GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
            string? pollID = PollDTO.FromJSON_GetNewPollID(response);

            return pollID;
        }

 
        public async Task<IEnumerable<OptionInfoDTO>> GetPollByID(string pollID)
        {
            string source = BaseSource + "/get/poll/{poll_id}?poll_id=" + pollID;
            string response = await GetJsonStringFromEndpointGet(ApiKey, source);
            return OptionInfoDTO.FromJSON(response);
        }

 
        public async Task CreateVoteForTrack(string pollID, string optionID, string username)
        {
            string bodyRequest = JsonConvert.SerializeObject(
              new
              {
                  poll_id = pollID,
                  option_id = optionID,
                  identifier = username
              }
            );
            //https://api.pollsapi.com/v1/create/vote
            string source = BaseSource + "/create/vote";
            await GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
        }

        //NEEDS SAFEGUARD CHECK
        public async Task<VoteIdentifierInfoDTO> GetSpecificUserVoteForAGivenPlaylist(string pollID, string username)
        {
            //https://api.pollsapi.com/v1/get/votes-with-identifier/truechad@gmail.com?offset=0&limit=100

            string source = BaseSource + "/get/votes-with-identifier/" + username + "?offset=0&limit=100";
            string response = await GetJsonStringFromEndpointGet(ApiKey, source);

            return VoteIdentifierInfoDTO.FromJson(response, pollID);
        }

        public async Task RemoveVote(string voteID)
        {
            //https://api.pollsapi.com/v1/remove/vote
            string source = BaseSource + "/remove/vote";
            string bodyRequest = JsonConvert.SerializeObject(
             new
             {
                 vote_id = voteID,
             }
           );

            await GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
        }

        //Did not crash when there was no poll with the given ID (potentially safe to try that live).
        public async Task RemovePoll(string pollID)
        {
            string source = BaseSource + "/remove/poll";
            string bodyRequest = JsonConvert.SerializeObject(
             new
             {
                 poll_id = pollID,
             }
           );

            await GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
        }

        public async Task<IEnumerable<PollDTO>> GetAllPolls()
        {
            //https://api.pollsapi.com/v1/get/polls?offset=0&limit=25
            string source = BaseSource + "/get/polls?offset=0&limit=100";
            string response = await GetJsonStringFromEndpointGet(ApiKey, source);
            return PollDTO.FromJSON_GetAllPolls(response);

        }
    }
}
