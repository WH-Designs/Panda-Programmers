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
using Microsoft.IdentityModel.Tokens;

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

        public string GetUriAsync(){
            var loginRequest = new LoginRequest(
            new Uri(Uri), ClientId, LoginRequest.ResponseType.Code)
            {
            Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserReadPrivate, Scopes.UserTopRead, Scopes.PlaylistModifyPrivate, 
                Scopes.PlaylistModifyPublic}
            };
            var uri = loginRequest.ToUri();
            
            return uri.AbsoluteUri;
        }

        public async Task<Listener> GetCallbackAsync(string code, Listener listener)
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

        public async Task<PrivateUser> GetAuthUserAsync()
        {
            authUser.Me = await Spotify.UserProfile.Current();
            return authUser.Me;
        }

        public async Task<SearchResponse> GetSearchResultsAsync(string searchQuery) 
        {
            SearchRequest.Types types = SearchRequest.Types.All;

            SearchRequest request = new SearchRequest(types, searchQuery);
            SearchResponse response = await Spotify.Search.Item(request);

            return response;
        }



        public async Task<List<FullArtist>> GetAuthTopArtistsAsync()
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

        public async Task<RecommendationGenresResponse> GetSeedGenresAsync()
        {
            var currentGenres = await Spotify.Browse.GetRecommendationGenres();
            return currentGenres;
        }



        public async Task<RecommendationsResponse> GetRecommendationsAsync(RecommendDTO recommendDTO)
        {
            RecommendationsRequest recommendationsRequest = new RecommendationsRequest();
            recommendationsRequest.Market = recommendDTO.market;
            recommendationsRequest.Limit = recommendDTO.limit;

            foreach(var track in recommendDTO.seed)
            {
                recommendationsRequest.SeedTracks.Add(track);
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

        public async Task<List<FullTrack>> ConvertToFullTrackAsync(List<SimpleTrack> tracks)
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

        public async Task<List<string>> SearchTopGenrePlaylistTrack(string genre)
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

        public static IUserProfileClient GetUserProfileClientAsync() 
        {
            return Spotify.UserProfile;
        }

        public static IPlaylistsClient GetPlaylistsClientAsync() 
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



        public async Task<List<UserTrackDTO>> GetAuthTopTracksAsync()
        {

            PersonalizationTopRequest Request = new PersonalizationTopRequest();
            Request.Limit = 20;
            var topTracks = await Spotify.Personalization.GetTopTracks(Request);
            var topTracksList = topTracks.Items;


            List<UserTrackDTO> TracksToReturn = new List<UserTrackDTO>();
            UserTrackDTO IndividualTrack = new UserTrackDTO();

            foreach (FullTrack track in topTracksList)
            {
                IndividualTrack = new UserTrackDTO();
                IndividualTrack.Title = track.Name;
                IndividualTrack.LinkToTrack = track.ExternalUrls["spotify"];
                IndividualTrack.Uri = track.Uri;
                if (track.Album.Images.IsNullOrEmpty() == false)
                {
                    IndividualTrack.ImageURL = track.Album.Images[0].Url;
                }
                else
                {
                    IndividualTrack.ImageURL = null;
                }
                TracksToReturn.Add(IndividualTrack);
            }

            return TracksToReturn;
        }

        public async Task<List<UserPlaylistDTO>> GetAuthFeatPlaylistsAsync()
        {
            PrivateUser CurUser = new PrivateUser();
            FeaturedPlaylistsRequest RequestParameters = new FeaturedPlaylistsRequest
            {
                Limit = 20,
            };
            CurUser = await Spotify.UserProfile.Current();
            try
            {
                RequestParameters.Country = CurUser.Country;
            }
            catch (NullReferenceException)
            {
                RequestParameters.Country = "NA";
            }

            List<UserPlaylistDTO> PlaylistsToReturn = new List<UserPlaylistDTO>();
            UserPlaylistDTO IndividualPlaylist = new UserPlaylistDTO();

            FeaturedPlaylistsResponse FeaturedPlaylists = await Spotify.Browse.GetFeaturedPlaylists(RequestParameters);
            foreach (var playlist in FeaturedPlaylists.Playlists.Items)
            {
                IndividualPlaylist = new UserPlaylistDTO();
                IndividualPlaylist.Name = playlist.Name;
                IndividualPlaylist.LinkToPlaylist = playlist.ExternalUrls["spotify"];
                IndividualPlaylist.Uri = playlist.Uri;

                if (playlist.Images.IsNullOrEmpty() == false)
                {
                    IndividualPlaylist.ImageURL = playlist.Images.First().Url;
                }
                else
                {
                    IndividualPlaylist.ImageURL = null;
                }
                PlaylistsToReturn.Add(IndividualPlaylist);
            }

            return PlaylistsToReturn;

        }


        public async Task<List<UserPlaylistDTO>> GetAuthPersonalPlaylistsAsync()
        {
            List<SimplePlaylist> PersonalPlaylists = new List<SimplePlaylist>();

            PlaylistCurrentUsersRequest RequestParameters = new PlaylistCurrentUsersRequest
            {
                Limit = 20
            };

            var currentUsersPlaylists = await Spotify.Playlists.CurrentUsers(RequestParameters);
            PersonalPlaylists = currentUsersPlaylists.Items;

            List<UserPlaylistDTO> UserPlaylists = new List<UserPlaylistDTO>();
            UserPlaylistDTO Playlist = new UserPlaylistDTO();

            foreach (var item in currentUsersPlaylists.Items)
            {
                Playlist = new UserPlaylistDTO();
                Playlist.Name = item.Name;
                Playlist.LinkToPlaylist = item.ExternalUrls["spotify"];
                Playlist.Uri = item.Uri;
                Playlist.ID = item.Id;
                if (item.Images.IsNullOrEmpty() == false)
                {
                    Playlist.ImageURL = item.Images[0].Url;
                }
                else
                {
                    Playlist.ImageURL = null;
                }
                UserPlaylists.Add(Playlist);

            }

            return UserPlaylists;
        }

        public async Task<List<FullTrack>> GetTopTracksAsync()
        {

            PersonalizationTopRequest Request = new PersonalizationTopRequest();
            Request.Limit = 20;
            var topTracks = await Spotify.Personalization.GetTopTracks(Request);
            var topTracksList = topTracks.Items;          

            return topTracksList;
        }

        public async Task<FullPlaylist> GetPlaylistFromIDAsync(string playlistID) {
            FullPlaylist wantedPlaylist = await Spotify.Playlists.Get(playlistID);
            return wantedPlaylist;
        }
    }
}