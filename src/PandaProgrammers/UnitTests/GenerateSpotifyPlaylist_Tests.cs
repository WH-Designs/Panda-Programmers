using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Moq;
using MusicCollaborationManager.Services.Concrete;
using SpotifyAPI.Web;

namespace UnitTests
{
    public class GenerateSpotifyPlaylist_Tests
    {
        const string USER_ID = "1234";
        const string NAME_OF_PLAYLIST = "MCM Playlist";

        private List<string> _trackUris;


        private Mock<SpotifyAuthService> _spotifyAuthService;
        private Mock<IUserProfileClient> _userProfileClient;
        private Mock<IPlaylistsClient> _playlistsClient;

        [SetUp]
        public void Setup()
        {
            _trackUris = new List<string>();

            _userProfileClient = new Mock<IUserProfileClient>();
            _spotifyAuthService = new Mock<SpotifyAuthService>();
            _playlistsClient = new Mock<IPlaylistsClient>();
        }

        [Test]
        public async Task CreateNewSpotifyPlaylistAsync_ShouldHaveMCMPLaylistAsName()
        {
            // Arrange

            FullPlaylist PlaylistToCreate = null;
            Mock<IUserProfileClient> UserProfileClient = new Mock<IUserProfileClient>();
            UserProfileClient.Setup(x => x.Current(default)).Returns(
              Task.FromResult(new PrivateUser
              {
                  Id = USER_ID
              })
            );

            Mock<IPlaylistsClient> PlaylistsClient = new Mock<IPlaylistsClient>();
            PlaylistCreateRequest CreationRequest = new PlaylistCreateRequest(NAME_OF_PLAYLIST);

            PlaylistsClient.Setup(x => x.Create(USER_ID, CreationRequest, default)).Returns(
              Task.FromResult(new FullPlaylist
              {
                  Name = NAME_OF_PLAYLIST
              })
            );


            //Act

            PlaylistToCreate = await SpotifyAuthService.CreateNewSpotifyPlaylistAsync(CreationRequest, UserProfileClient.Object, PlaylistsClient.Object);



            //Assert

            Assert.That(PlaylistToCreate != null);
            Assert.That(PlaylistToCreate.Name.Equals("MCM Playlist"));
        }
    }
}
