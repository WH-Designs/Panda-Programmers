using Microsoft.EntityFrameworkCore.Infrastructure;
using MusicCollaborationManager.Models;
using MusicCollaborationManager.Services;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.Services.Abstract;
using SpotifyAPI.Web;
using Moq;

namespace UnitTests;

public class SpotifyAuthServiceTests 
{
    private SpotifyAuthService _spotifyService;
    private Mock<ISpotifyClient> _spotifyClient;

    [SetUp]
    public void Setup()
    {
        _spotifyService = new SpotifyAuthService("", "", "");
        _spotifyClient = new Mock<ISpotifyClient>();
    }
    
    [Test]
    public async Task GetAuthUserAsyncReturnsCorrectUser()
    {

        _spotifyClient.Setup(u => u.UserProfile.Current(It.IsAny<System.Threading.CancellationToken>())).Returns(
            Task.FromResult(new PrivateUser
                {
                    Id = "1234",
                    DisplayName = "Display Name",
                    Email = "example@email.com"
                })
        );

        PrivateUser test_user = await _spotifyService.GetAuthUserAsync(_spotifyClient.Object);

        Assert.AreEqual(test_user.Id, "1234");
        Assert.AreEqual(test_user.DisplayName, "Display Name");
        Assert.AreEqual(test_user.Email, "example@email.com");
    }

    [Test]
    public async Task GetUserDisplayNameReturnsCorrectDisplayName()
    {
        _spotifyClient.Setup(u => u.UserProfile.Get("test_id", It.IsAny<System.Threading.CancellationToken>())).Returns(
            Task.FromResult(new PublicUser
                {
                    DisplayName = "Display Name",
                })
        );

        string testDisplayName = await _spotifyService.GetUserDisplayName("test_id", _spotifyClient.Object);

        Assert.AreEqual(testDisplayName, "Display Name");
    }

    [Test]
    public async Task LikePlaylistReturnsTrue()
    {
        _spotifyClient.Setup(u => u.Follow.FollowPlaylist("playlist_id", It.IsAny<System.Threading.CancellationToken>())).Returns(
            Task.FromResult(true));

        bool testBoolean = await _spotifyService.LikePlaylist("playlist_id", _spotifyClient.Object);

        Assert.AreEqual(testBoolean, true);
    }

    [Test]
    public async Task GetUserPlaylistsReturnsCorrectPlaylists()
    {
        List<SimplePlaylist> playlists = new List<SimplePlaylist>();
        Paging<SimplePlaylist> pagingPlaylists = new Paging<SimplePlaylist>();

        _spotifyClient.Setup(u => u.Playlists.GetUsers("user_id", It.IsAny<System.Threading.CancellationToken>())).Returns(
            Task.FromResult(new Paging<SimplePlaylist>{ Items = playlists }));

        List<SimplePlaylist> testPlaylistList = await _spotifyService.GetUserPlaylists("user_id", _spotifyClient.Object);

        Assert.AreEqual(testPlaylistList, playlists);
    }

    [Test]
    public async Task GetSearchResultsAsyncReturnsResponseObject()
    {
        string query = "test";

        SearchRequest.Types types = SearchRequest.Types.All;
        SearchResponse returnResponse = new SearchResponse();

        _spotifyClient.Setup(u => u.Search.Item(new SearchRequest(types, query), It.IsAny<System.Threading.CancellationToken>())).Returns(
            Task.FromResult(returnResponse));

        SearchResponse testSearchResponse = await _spotifyService.GetSearchResultsAsync(query, _spotifyClient.Object);

        Assert.AreEqual(testSearchResponse, null); // should be comparing to returnResponse
    }

    [Test]
    public async Task GetAuthTopArtistsAsyncReturnsCorrectArtists()
    {
        List<FullArtist> artistList = new List<FullArtist>();
        FullArtist testArtist = new FullArtist();

        artistList.Add(testArtist);

        _spotifyClient.Setup(u => u.Personalization.GetTopArtists(It.IsAny<System.Threading.CancellationToken>())).Returns(
            Task.FromResult(new Paging<FullArtist>(){Items = artistList}));

        List<FullArtist> testTopArtists = await _spotifyService.GetAuthTopArtistsAsync(_spotifyClient.Object);

        Assert.AreEqual(testTopArtists, artistList);
    }
    [Test]
    public async Task GetAuthTopArtistsAsyncReturnsCorrectArtistsWhenPersonalizationReturnsWithNoArtists()
    {
        // List<FullArtist> artistList = new List<FullArtist>();

        // List<string> artistIDs = new List<string>();
        // artistIDs.Add("04gDigrS5kc9YWfZHwBETP");
        // ArtistsRequest artistRequest = new ArtistsRequest(artistIDs);

        // _spotifyClient.Setup(u => u.Personalization.GetTopArtists(It.IsAny<System.Threading.CancellationToken>())).Returns(
        //     Task.FromResult(new Paging<FullArtist>()));

        // _spotifyClient.Setup(u => u.Artists.GetSeveral(artistRequest, It.IsAny<System.Threading.CancellationToken>())).Returns(
        //     Task.FromResult(new ArtistsResponse()));

        // List<FullArtist> testTopArtists = await _spotifyService.GetAuthTopArtistsAsync(_spotifyClient.Object);

        // Assert.AreEqual(testTopArtists[0].Id, "04gDigrS5kc9YWfZHwBETP");
    }


    [Test]
    public async Task GetAuthRelatedArtistsAsync()
    {
        
    }

    [Test]
    public async Task GetSeedGenresAsync()
    {
        
    }

    [Test]
    public async Task GetRecommendationsAsync()
    {
        
    }

    [Test]
    public async Task GetRecommendationsArtistBasedAsync()
    {
        
    }

    [Test]
    public async Task GetRecommendationsGenreBased()
    {
        
    }

    [Test]
    public async Task ConvertToFullTrackAsync()
    {
        
    }

    [Test]
    public async Task SearchTopGenrePlaylistTrack()
    {
        
    }

    [Test]
    public async Task GetUserProfileClientAsync()
    {
        
    }

    [Test]
    public async Task GetPlaylistsClientAsync()
    {
        
    }

    [Test]
    public async Task AddSongsToPlaylistAsync()
    {
        
    }

    [Test]
    public async Task ChangeCoverForPlaylist()
    {
        
    }

    [Test]
    public async Task CreateNewSpotifyPlaylistAsync()
    {
        
    }

    [Test]
    public async Task GetAuthTopTracksAsync()
    {
        
    }

    [Test]
    public async Task GetAuthFeatPlaylistsAsync()
    {
        
    }

    [Test]
    public async Task GetAuthPersonalPlaylistsAsync()
    {
        
    }

    [Test]
    public async Task GetTopTracksAsync()
    {
        
    }

    [Test]
    public async Task GetPlaylistFromIDAsync()
    {
        
    }

    [Test]
    public async Task GetTracksClientAsync()
    {
        
    }

    [Test]
    public async Task GetSpotifyTrackByID()
    {
        
    }

    [Test]
    public async Task GetArtistById()
    {
        
    }
}