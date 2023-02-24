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

        //public static SpotifyClientConfig DefaultConfig = SpotifyClientConfig.CreateDefault();
        public AuthorizedUserDTO authUser { get; set; }
        public string Uri { get; set; }


        public SpotifyAuthService(string id, string secret, string redirect)
        {
            ClientId = id;
            ClientSecret = secret;
            Uri = redirect;
        }

        public string GetUri(){
            var loginRequest = new LoginRequest(
            new Uri(Uri), ClientId, LoginRequest.ResponseType.Code)
            {
            Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserReadPrivate, Scopes.UserTopRead}
            };
            var uri = loginRequest.ToUri();
            
            return uri.AbsoluteUri;
        }

        public async Task<SpotifyClient> GetCallback(string code)
        {
            Uri uri = new Uri(Uri);
            var response = await new OAuthClient().RequestToken(new AuthorizationCodeTokenRequest(ClientId, ClientSecret, code, uri));
            var config = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new AuthorizationCodeAuthenticator(ClientId, ClientSecret, response));

            var authenticatedSpotify = new SpotifyClient(config);
            Spotify = authenticatedSpotify;

            return authenticatedSpotify;
        }

        public async Task<PrivateUser> GetAuthUser()
        {
            return await Spotify.UserProfile.Current();
        }

        public async Task<List<FullTrack>> GetAuthUserTopTracks()
        {
            var topTracks = await Spotify.Personalization.GetTopTracks();
            var topTracksList = topTracks.Items;

            if (topTracksList.Count == 0) {
                List<string> trackIDs = new List<string>();

                trackIDs.Add("4cktbXiXOapiLBMprHFErI");
                trackIDs.Add("6KBYk8OFtod7brGuZ3Y67q");
                trackIDs.Add("2iuZJX9X9P0GKaE93xcPjk");
                trackIDs.Add("5zFglKYiknIxks8geR8rcL");
                trackIDs.Add("0tuyEYTaqLxE41yGHSsXjy");
                
                TracksRequest trackReq = new TracksRequest(trackIDs);

                var topGenTracks = await Spotify.Tracks.GetSeveral(trackReq);
                var returnTracks = topGenTracks.Tracks.ToList();
                return returnTracks;
            }

            return topTracksList;
        }
        public async Task<FeaturedPlaylistsResponse> GetAuthFeatPlaylists()
        {
            PrivateUser CurUser = await Spotify.UserProfile.Current();
            FeaturedPlaylistsRequest RequestParameters = new FeaturedPlaylistsRequest
            {
                Limit = 5,
                Country = CurUser.Country,
            };

            if (CurUser.Country == "US")
                RequestParameters.Limit = 10;

            FeaturedPlaylistsResponse FeaturedPlaylists = await Spotify.Browse.GetFeaturedPlaylists(RequestParameters);
            
            if (CurUser.Country == "US") 
                FeaturedPlaylists.Playlists.Items.Reverse();

            return FeaturedPlaylists;
        }

        public async Task<List<SimplePlaylist>> GetAuthPersonalPlaylists()
        {
            List<SimplePlaylist> PersonalPlaylists = new List<SimplePlaylist>();

            PlaylistCurrentUsersRequest RequestParameters = new PlaylistCurrentUsersRequest
            {
                Limit = 5
            };

            var currentUsersPlaylists = await Spotify.Playlists.CurrentUsers(RequestParameters);
            PersonalPlaylists = currentUsersPlaylists.Items;
        
            return PersonalPlaylists;
        }

        public async Task<RecommendationGenresResponse> GetSeedGenres()
        {
            var currentGenres = await Spotify.Browse.GetRecommendationGenres();

            return currentGenres;
        }

        public async Task<RecommendationsResponse> GetRecommendations(RecommendDTO recommendDTO)
        {
            RecommendationsRequest recommendationsRequest = new RecommendationsRequest();
            recommendationsRequest.Market = recommendDTO.market;
            recommendationsRequest.Limit = recommendDTO.limit;
            recommendationsRequest.SeedGenres.Add(recommendDTO.genre);

            recommendationsRequest.Target.Add("acousticness", recommendDTO.target_acousticness.ToString());
            recommendationsRequest.Target.Add("danceability", recommendDTO.target_danceability.ToString());
            recommendationsRequest.Target.Add("energy", recommendDTO.target_energy.ToString());
            recommendationsRequest.Target.Add("instrumentalness", recommendDTO.target_instrumentalness.ToString());
            recommendationsRequest.Target.Add("liveness", recommendDTO.target_liveness.ToString());
            recommendationsRequest.Target.Add("popularity", recommendDTO.target_popularity.ToString());
            recommendationsRequest.Target.Add("speechiness", recommendDTO.target_speechiness.ToString());
            recommendationsRequest.Target.Add("temp", recommendDTO.target_tempo.ToString());
            recommendationsRequest.Target.Add("valence", recommendDTO.target_valence.ToString());

            var recommendations = await Spotify.Browse.GetRecommendations(recommendationsRequest);

            return recommendations;
        }

    }
}