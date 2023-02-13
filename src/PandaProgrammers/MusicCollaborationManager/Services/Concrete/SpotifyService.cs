using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace MusicCollaborationManager.Services.Concrete
{
    public class SpotifyService : ISpotifyService
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }

        public SpotifyService(string id, string secret) 
        {
            ClientId = id;
            ClientSecret = secret;
        }

        public static async Task<ArtistsTopTracksResponse> GetVisitorTracks(string artistId, string tracksFromRegion)
        {
            var Config = SpotifyClientConfig
              .CreateDefault()
              .WithAuthenticator(new ClientCredentialsAuthenticator(
                  ClientId,
              ClientSecret));

            var Spotify = new SpotifyClient(Config);
            var RequestParameters = new ArtistsTopTracksRequest(tracksFromRegion);

            return await Spotify.Artists.GetTopTracks(artistId, RequestParameters);
        }


        public static async Task<FeaturedPlaylistsResponse> GetVisitorPlaylists()
        {
            var Config = SpotifyClientConfig
              .CreateDefault()
              .WithAuthenticator(new ClientCredentialsAuthenticator(
                  ClientId,
              ClientSecret));

            var Spotify = new SpotifyClient(Config);


            const int LIMIT = 5;
            var RequestParameters = new FeaturedPlaylistsRequest
            {
                Limit = LIMIT,
            };

            var FeaturedPlaylists = await Spotify.Browse.GetFeaturedPlaylists(RequestParameters);

            return FeaturedPlaylists;
        }


        Task<ArtistsTopTracksResponse> ISpotifyService.GetVisitorTracks(string artistId, string tracksFromRegion)
        {
            return GetVisitorTracks(artistId, tracksFromRegion);
        }

        Task<FeaturedPlaylistsResponse> ISpotifyService.GetVisitorPlaylists() 
        {
            return GetVisitorPlaylists();
        }
    }
}
