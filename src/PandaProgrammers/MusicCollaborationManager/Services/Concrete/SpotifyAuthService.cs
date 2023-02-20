using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using MusicCollaborationManager.Models.DTO;

namespace MusicCollaborationManager.Services.Concrete
{
    public class SpotifyAuthService
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        private static SpotifyClientConfig Config { get; set; }
        private static SpotifyClient Spotify { get; set; }
        public AuthorizedUserDTO authUser { get; set; }


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

        public async Task<SpotifyClient> GetCallback(string code)
        {
            Uri uri = new Uri("http://localhost:5000/home/callback");
            var response = await new OAuthClient().RequestToken(new AuthorizationCodeTokenRequest(ClientId, ClientSecret, code, uri));
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new AuthorizationCodeAuthenticator(ClientId, ClientSecret, response));

            var authenticatedSpotify = new SpotifyClient(config);
            Spotify = authenticatedSpotify;

            // var user = authenticatedSpotify.UserProfile.Current();
            // var user_playlists = authenticatedSpotify.Playlists.CurrentUsers();
            // var user_playlists_and_information = (user, user_playlists);

            return authenticatedSpotify;
        }

        public async Task<PrivateUser> GetAuthUser()
        {
            return await Spotify.UserProfile.Current();
        }

        // public async Task GetCurrentDisplayName(); 
        // {
        //     //authUser.Me = await Spotify.UserProfile.Current();
        //     return null
        // }
    }
}