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
using SpotifyAPI.Web.Http;

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

        public string GetUriAsync()
        {
            var loginRequest = new LoginRequest(
            new Uri(Uri), ClientId, LoginRequest.ResponseType.Code)
            {
                Scope = new[] { Scopes.PlaylistReadPrivate, Scopes.PlaylistReadCollaborative, Scopes.UserReadPrivate, Scopes.UserTopRead, Scopes.PlaylistModifyPrivate,
                Scopes.PlaylistModifyPublic, Scopes.UgcImageUpload}
            };
            var uri = loginRequest.ToUri();

            return uri.AbsoluteUri;
        }

        public async Task<Listener> GetCallbackAsync(string code, Listener listener)
        {
            Uri uri = new Uri(Uri);

            if (listener.AuthToken == null && listener.SpotifyId == null && listener.AuthRefreshToken == null)
            {

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

            try
            {
                var refreshResponse = await new OAuthClient().RequestToken(new AuthorizationCodeRefreshRequest(ClientId, ClientSecret, listener.AuthRefreshToken));

                listener.AuthToken = refreshResponse.AccessToken;

                var authenticatedSpotify = new SpotifyClient(listener.AuthToken);
                Spotify = authenticatedSpotify;

                return listener;

            }
            catch (APIUnauthorizedException e)
            {
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

        public async Task<String> GetUserDisplayName(string spotifyID)
        {
            PublicUser user = await Spotify.UserProfile.Get(spotifyID);
            return user.DisplayName;
        }

        public async Task<bool> LikePlaylist(string playlistID)
        {
            var resp = await Spotify.Follow.FollowPlaylist(playlistID);
            return resp;
        }

        public async Task<List<SimplePlaylist>> GetUserPlaylists(string spotifyID)
        {
            List<SimplePlaylist> playlists = new List<SimplePlaylist>();

            Paging<SimplePlaylist> pagingPlaylists = await Spotify.Playlists.GetUsers(spotifyID);
            playlists = pagingPlaylists.Items;

            playlists = playlists.Where(p => p.Owner.Id == spotifyID).ToList();

            return playlists;
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

            if (returnArtists.Count == 0)
            {
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

        public async Task<List<FullArtist>> GetAuthRelatedArtistsAsync(List<FullArtist> TopArtists)
        {
            var relatedArtists = new List<FullArtist>();

            foreach (FullArtist artist in TopArtists)
            {
                var newArtists = await Spotify.Artists.GetRelatedArtists(artist.Id);
                foreach (FullArtist newArtist in newArtists.Artists)
                {
                    relatedArtists.Add(newArtist);
                }
                relatedArtists.Add(artist);
            }

            return relatedArtists;
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

            foreach (var track in recommendDTO.seed)
            {
                recommendationsRequest.SeedTracks.Add(track);
                if (recommendationsRequest.SeedTracks.Count >= 5) { break; }
            }
            if (recommendDTO.target_valence != 0)
            {
                recommendationsRequest.Target.Add("valence", recommendDTO.target_valence.ToString());
            }
            if (recommendDTO.target_acousticness != 0)
            {
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

        public async Task<RecommendationsResponse> GetRecommendationsArtistBasedAsync(RecommendDTO recommendDTO)
        {
            RecommendationsRequest recommendationsRequest = new RecommendationsRequest();
            recommendationsRequest.Market = recommendDTO.market;
            recommendationsRequest.Limit = recommendDTO.limit;

            foreach (var artist in recommendDTO.artistSeed)
            {
                recommendationsRequest.SeedArtists.Add(artist);
                if (recommendationsRequest.SeedArtists.Count >= 5) { break; }
            }

            var recommendations = await Spotify.Browse.GetRecommendations(recommendationsRequest);
            return recommendations;

        }

        public async Task<RecommendationsResponse> GetRecommendationsGenreBased(RecommendDTO recommendDTO)
        {
            RecommendationsRequest recommendationsRequest = new RecommendationsRequest();
            recommendationsRequest.Market = recommendDTO.market;
            recommendationsRequest.Limit = recommendDTO.limit;
            foreach (var genre in recommendDTO.genre)
            {
                recommendationsRequest.SeedGenres.Add(genre);
                if (recommendationsRequest.SeedGenres.Count >= 1) { break; }
            }

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

            for (int i = 0; i < 5; i++)
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

        public async Task<bool> ChangeCoverForPlaylist(string playlistID, string imgAsBase64)
        {
            return await Spotify.Playlists.UploadCover(playlistID,
                "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");
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
                IndividualPlaylist.ID = playlist.Id;

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

        public async Task<FullPlaylist> GetPlaylistFromIDAsync(string playlistID)
        {
            FullPlaylist wantedPlaylist = await Spotify.Playlists.Get(playlistID);
            return wantedPlaylist;
        }

        public static ITracksClient GetTracksClientAsync()
        {
            return Spotify.Tracks;
        }

        public async Task<FullTrack> GetSpotifyTrackByID(string trackID, ITracksClient tracksClient)
        {
            return await tracksClient.Get(trackID);
        }
    }
}