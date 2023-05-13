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

        public string GetJsonStringFromEndpointPost(string bearerToken, string uri, string request)
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

        public string GetJsonStringFromEndpointGet(string bearerToken, string uri)
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
                return null;
            }
        }

        public string CreatePollForSpecificPlaylist(string spotifyPlaylistID)
        {
            string source = BaseSource + "/create/poll";
            string bodyRequest = "{\r\n    \"question\": \"Add to playlist?\",\r\n    \"identifier\": \"" + spotifyPlaylistID + "\",\r\n    \"options\": [\r\n        {\r\n            \"text\": \"Yes\"\r\n        },\r\n        {\r\n            \"text\": \"No\"\r\n        }\r\n    ]\r\n}";

            string response = GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
            string pollID = PollDTO.FromJSON_GetNewPollID(response);

            return pollID;
        }

 
        public IEnumerable<OptionInfoDTO> GetPollOptionsByPollID(string pollID)
        {
            string source = BaseSource + "/get/poll/{poll_id}?poll_id=" + pollID;
            string response = GetJsonStringFromEndpointGet(ApiKey, source);
            return OptionInfoDTO.FromJSON(response);
        }

 
        public void CreateVoteForTrack(string pollID, string optionID, string username)
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
            GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
        }

        //NEEDS SAFEGUARD CHECK
        public VoteIdentifierInfoDTO GetSpecificUserVoteForAGivenPlaylist(string pollID, string username)
        {
            //https://api.pollsapi.com/v1/get/votes-with-identifier/truechad@gmail.com?offset=0&limit=100

            string source = BaseSource + "/get/votes-with-identifier/" + username + "?offset=0&limit=100";
            string? response = GetJsonStringFromEndpointGet(ApiKey, source);

            if(response == null) 
            {
                return null;
            }
      
            return VoteIdentifierInfoDTO.FromJson(response, pollID, username);
        }

        public void RemoveVote(string voteID)
        {
            //https://api.pollsapi.com/v1/remove/vote
            string source = BaseSource + "/remove/vote";
            string bodyRequest = JsonConvert.SerializeObject(
             new
             {
                 vote_id = voteID,
             }
           );

            GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
        }

        //Did not crash when there was no poll with the given ID (potentially safe to try that live).
        public void RemovePoll(string pollID)
        {
            string source = BaseSource + "/remove/poll";
            string bodyRequest = JsonConvert.SerializeObject(
             new
             {
                 poll_id = pollID,
             }
           );

            GetJsonStringFromEndpointPost(ApiKey, source, bodyRequest);
        }

        public IEnumerable<PollDTO> GetAllPolls()
        {
            //https://api.pollsapi.com/v1/get/polls?offset=0&limit=25
            string source = BaseSource + "/get/polls?offset=0&limit=100";
            string response = GetJsonStringFromEndpointGet(ApiKey, source);
            return PollDTO.FromJSON_GetAllPolls(response);

        }
    }
}
