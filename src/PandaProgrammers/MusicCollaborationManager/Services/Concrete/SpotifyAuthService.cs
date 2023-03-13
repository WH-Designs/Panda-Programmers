using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using System.Threading;
using System.Threading.Tasks;
using SpotifyAPI.Web.Auth;
using Microsoft.AspNetCore.Authentication;
using System.Diagnostics;
using MusicCollaborationManager.Models.DTO;
using MusicCollaborationManager.Models;
using System;
using MusicCollaborationManager.Utilities;

namespace MusicCollaborationManager.Services.Concrete
{
    public class SpotifyAuthService
    {
        public static string ClientId { get; set; }
        public static string ClientSecret { get; set; }
        private static SpotifyClientConfig Config { get; set; }
        public static SpotifyClient Spotify { get; set; }
        public AuthorizedUserDTO authUser { get; set; }
        public string Uri { get; set; }


        public SpotifyAuthService(string id, string secret, string redirect)
        {
            ClientId = id;
            ClientSecret = secret;
            Uri = redirect;
            authUser = new AuthorizedUserDTO();
        }

        public string GetUri(){
            var loginRequest = new LoginRequest(
            new Uri(Uri), ClientId, LoginRequest.ResponseType.Code)
            {
            Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserReadPrivate, Scopes.UserTopRead, Scopes.PlaylistModifyPrivate, 
                Scopes.PlaylistModifyPublic}
            };
            var uri = loginRequest.ToUri();
            
            return uri.AbsoluteUri;
        }

        public async Task<Listener> GetCallback(string code, Listener listener)
        {
            Uri uri = new Uri(Uri);
            
            if (listener.AuthToken == null && listener.SpotifyId == null && listener.AuthRefreshToken == null) {
                
                var response = await new OAuthClient().RequestToken(new AuthorizationCodeTokenRequest(ClientId, ClientSecret, code, uri));
                listener.AuthToken = response.AccessToken;
                listener.AuthRefreshToken = response.RefreshToken;

                var config = SpotifyClientConfig
                    .CreateDefault()
                    .WithAuthenticator(new AuthorizationCodeAuthenticator(ClientId, ClientSecret, response));

                var authenticatedNewSpotify = new SpotifyClient(config);
                Spotify = authenticatedNewSpotify;

                return listener;
            }

            try {                
                var refreshResponse = await new OAuthClient().RequestToken(new AuthorizationCodeRefreshRequest(ClientId, ClientSecret, listener.AuthRefreshToken));

                listener.AuthToken = refreshResponse.AccessToken;          

                var authenticatedSpotify = new SpotifyClient(listener.AuthToken);
                Spotify = authenticatedSpotify;

                return listener;                

            } catch (APIUnauthorizedException e){
                Console.WriteLine(e.Message);

                listener.AuthRefreshToken = "";

                return listener;
            }
        }

        public async Task<PrivateUser> GetAuthUser()
        {
            authUser.Me = await Spotify.UserProfile.Current();
            return authUser.Me;
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

        public async Task<List<FullArtist>> GetAuthTopArtists()
        {
            var topArtists = await Spotify.Personalization.GetTopArtists();
            List<FullArtist> returnArtists = topArtists.Items;
            
            if (returnArtists.Count == 0) {
                List<string> artistIDs = new List<string>();

                artistIDs.Add("04gDigrS5kc9YWfZHwBETP");
                artistIDs.Add("1Xyo4u8uXC1ZmMpatF05PJ");
                artistIDs.Add("5cj0lLjcoR7YOSnhnX0Po5");
                artistIDs.Add("06HL4z0CvFAxyc27GXpf02");
                artistIDs.Add("66CXWjxzNUsdJxJ2JdwvnR");
                
                ArtistsRequest artistReq = new ArtistsRequest(artistIDs);

                var topGenArtists = await Spotify.Artists.GetSeveral(artistReq);
                var returnGenArtists = topGenArtists.Artists.ToList();
                return returnGenArtists;
            } 
            return returnArtists;
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

        public async Task<List<SimplePlaylist>> GetFeatPlaylists()
        {
            PrivateUser CurUser = new PrivateUser();
            FeaturedPlaylistsRequest RequestParameters = new FeaturedPlaylistsRequest
            {
                Limit = 5,
            };
            try
            {
                CurUser = await Spotify.UserProfile.Current();
                RequestParameters.Country = CurUser.Country;
            }
            catch (NullReferenceException e) 
            {
                RequestParameters.Country = "NA";
            }


            if (RequestParameters.Country == "US")
                RequestParameters.Limit = 10;

            FeaturedPlaylistsResponse FeaturedPlaylists = await Spotify.Browse.GetFeaturedPlaylists(RequestParameters);
            if (FeaturedPlaylists == null) 
            {
                return null;
            }

            if (CurUser.Country == "US")
                FeaturedPlaylists.Playlists.Items.Reverse();

            return FeaturedPlaylists.Playlists.Items;
        }   

        public async Task<RecommendationsResponse> GetRecommendations(RecommendDTO recommendDTO)
        {
            RecommendationsRequest recommendationsRequest = new RecommendationsRequest();
            recommendationsRequest.Market = recommendDTO.market;
            recommendationsRequest.Limit = recommendDTO.limit;

            foreach(var artist in recommendDTO.seed)
            {
                recommendationsRequest.SeedTracks.Add(artist);
                if(recommendationsRequest.SeedTracks.Count >= 5) {break; }
            }
            //foreach (var genre in recommendDTO.genre)
            //{
            //    recommendationsRequest.SeedGenres.Add(genre);
            //    if (recommendationsRequest.SeedGenres.Count >= 1) { break; }
            //}
            if (recommendDTO.target_valence != 0)
            {
                recommendationsRequest.Target.Add("valence", recommendDTO.target_valence.ToString());
            }
            if (recommendDTO.target_acousticness != 0){
                recommendationsRequest.Target.Add("acousticness", recommendDTO.target_acousticness.ToString());
            }
            if (recommendDTO.target_danceability != 0)
            {
                recommendationsRequest.Target.Add("danceability", recommendDTO.target_danceability.ToString());
            }
            if (recommendDTO.target_energy != 0)
            {
                recommendationsRequest.Target.Add("energy", recommendDTO.target_energy.ToString());
            }
            if (recommendDTO.target_instrumentalness != 0)
            {
                recommendationsRequest.Target.Add("instrumentalness", recommendDTO.target_instrumentalness.ToString());
            }
            if (recommendDTO.target_liveness != 0)
            {
                recommendationsRequest.Target.Add("liveness", recommendDTO.target_liveness.ToString());
            }
            if (recommendDTO.target_popularity != 0)
            {
                recommendationsRequest.Target.Add("popularity", recommendDTO.target_popularity.ToString());
            }
            if (recommendDTO.target_speechiness != 0)
            {
                recommendationsRequest.Target.Add("speechiness", recommendDTO.target_speechiness.ToString());
            }
            if (recommendDTO.target_tempo != 0)
            {
                recommendationsRequest.Target.Add("tempo", recommendDTO.target_tempo.ToString());
            }

            var recommendations = await Spotify.Browse.GetRecommendations(recommendationsRequest);
            return recommendations;

        }

        public async Task<List<FullTrack>> ConvertToFullTrack(List<SimpleTrack> tracks)
        {
            List<string> trackIds = new List<string>();
            foreach (var track in tracks)
            {
                trackIds.Add(track.Id);
            }

            TracksRequest trackReq = new TracksRequest(trackIds);

            var genTracks = await Spotify.Tracks.GetSeveral(trackReq);
            var returnTracks = genTracks.Tracks;
            return returnTracks;

        }

        public async Task<List<string>> searchTopGenrePlaylistArtist(string genre)
        {
            GeneratorUtilities generatorUtilities = new GeneratorUtilities();
            SearchRequest.Types types = SearchRequest.Types.Playlist;
            List<string> trackIDs = new List<string>();

            SearchRequest request = new SearchRequest(types, genre);
            request.Limit = 10;
            SearchResponse response = await Spotify.Search.Item(request);

            for (int i=0; i < 5; i++)
            {
                string playlistID = response.Playlists.Items[generatorUtilities.rngValueInput(1, response.Playlists.Items.Count())].Id;
                var playlist = await Spotify.Playlists.GetItems(playlistID);

                FullTrack track = (FullTrack)playlist.Items[generatorUtilities.rngValueInput(1, playlist.Items.Count - 1)].Track;
                trackIDs.Add(track.Id);
            }                 

            return trackIDs;
        }

        public static IUserProfileClient GetUserProfileClient() 
        {
            return Spotify.UserProfile;
        }

        public static IPlaylistsClient GetPlaylistsClient() 
        {
            return Spotify.Playlists;
        }

        public async Task AddSongsToPlaylistAsync(FullPlaylist playlistToFill, List<string> trackUris)
        {
            PlaylistAddItemsRequest AddItemsRequest = new PlaylistAddItemsRequest(trackUris);
            await Spotify.Playlists.AddItems(playlistToFill.Id, AddItemsRequest);
        }

        public static async Task<FullPlaylist> CreateNewSpotifyPlaylistAsync(PlaylistCreateRequest createRequest, IUserProfileClient userProfileClient, IPlaylistsClient playlistsClient)
        {
            PrivateUser CurUser = await userProfileClient.Current();
            return await playlistsClient.Create(CurUser.Id, createRequest);
        }

    }
}