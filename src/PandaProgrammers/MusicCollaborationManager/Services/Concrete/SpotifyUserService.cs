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

        public SpotifyUserService(string id, string secret)
        {
            // ClientId = id;
            // ClientSecret = secret;

            // var loginRequest = new LoginRequest(new Uri("http://localhost:5191"), ClientId, LoginRequest.ResponseType.Code)
            // {
            //     Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative }
            // };
            
            // var uri = loginRequest.ToUri();
        }

        // public async Task GetCallback(string new_code)
        // {

        //     Uri uri = new Uri("http://localhost:5191");
        //     var response = await new OAuthClient().RequestToken(
        //         new AuthorizationCodeTokenRequest(ClientId, ClientSecret, new_code, uri)
        //     );

        //     var config = SpotifyClientConfig
        //         .CreateDefault()
        //         .WithAuthenticator(new AuthorizationCodeAuthenticator(ClientId, ClientSecret, response));

        //     var spotify = new SpotifyClient(config);
        // }
    }
}
