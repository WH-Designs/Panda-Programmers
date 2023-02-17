using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace MusicCollaborationManager.Services.Abstract
{
    public class SpotifyUserService : ISpotifyUserService
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        private static SpotifyClientConfig Config { get; set; }
        private static SpotifyClient Spotify { get; set; }


        public SpotifyUserService(string id, string secret)
        {
            ClientId = id;
            ClientSecret = secret;

            Config = SpotifyClientConfig
              .CreateDefault()
              .WithAuthenticator(new ClientCredentialsAuthenticator(
                  ClientId,
              ClientSecret));

            Spotify = new SpotifyClient(Config);
        }
    }
}
