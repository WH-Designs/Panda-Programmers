using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace MusicCollaborationManager.Services.Concrete
{
    public class SpotifyAuthService
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        private static SpotifyClientConfig Config { get; set; }
        private static SpotifyClient Spotify { get; set; }


        public SpotifyAuthService(string id, string secret)
        {
            ClientId = id;
            ClientSecret = secret;
        }

        public string GetUri(){
            var loginRequest = new LoginRequest(
            new Uri("http://localhost:5000/home/callback"), ClientId, LoginRequest.ResponseType.Code)
            {
            Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserReadPrivate }
            };
            var uri = loginRequest.ToUri();
            
            return uri.AbsoluteUri;
        }

        public async Task GetCallback(string code)
        {
            Uri uri = new Uri("http://localhost:5000/home/UserPage");
            var response = await new OAuthClient().RequestToken(new AuthorizationCodeTokenRequest(ClientId, ClientSecret, code, uri));
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new AuthorizationCodeAuthenticator(ClientId, ClientSecret, response));

            var authenticatedSpotify = new SpotifyClient(config);
            var user = authenticatedSpotify.UserProfile.Current();
        }
    }
}