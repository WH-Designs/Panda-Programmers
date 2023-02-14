using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;

namespace MusicCollaborationManager.Services.Concrete
{
    public class SpotifyVisitorService : ISpotifyVisitorService
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        private static SpotifyClientConfig Config { get; set; }
        private static SpotifyClient Spotify { get; set; }


        public SpotifyVisitorService(string id, string secret)
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

        public static async Task<ArtistsTopTracksResponse> GetVisitorTracks(string artistId, string tracksFromRegion)
        {
            ArtistsTopTracksRequest RequestParameters = new ArtistsTopTracksRequest(tracksFromRegion);

            return await Spotify.Artists.GetTopTracks(artistId, RequestParameters);
        }


        public static async Task<FeaturedPlaylistsResponse> GetVisitorPlaylists()
        {
            FeaturedPlaylistsRequest RequestParameters = new FeaturedPlaylistsRequest
            {
                Limit = 5,
            };

            var FeaturedPlaylists = await Spotify.Browse.GetFeaturedPlaylists(RequestParameters);
            return FeaturedPlaylists;
        }


        Task<ArtistsTopTracksResponse> ISpotifyVisitorService.GetVisitorTracks(string artistId, string tracksFromRegion)
        {
            return GetVisitorTracks(artistId, tracksFromRegion);
        }

        Task<FeaturedPlaylistsResponse> ISpotifyVisitorService.GetVisitorPlaylists()
        {
            return GetVisitorPlaylists();
        }
    }
}
