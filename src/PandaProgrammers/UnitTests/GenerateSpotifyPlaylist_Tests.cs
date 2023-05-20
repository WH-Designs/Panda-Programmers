using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Moq;
using MusicCollaborationManager.Services.Concrete;
using MusicCollaborationManager.ViewModels;
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

        [Test]
        public void EnsurePlaylistDescriptionSize_WithNullDescription_ShouldReturnNull() 
        {
            //Arrange
            string AiDescription = null;
            GeneratorsViewModel vm = new GeneratorsViewModel();
            vm.PlaylistDescription = AiDescription;

            //Act
            vm.PlaylistDescription = GeneratorsViewModel.EnsurePlaylistDescriptionSize(vm.PlaylistDescription);

            //Assert
            Assert.That(vm.PlaylistDescription, Is.Null); 
        }

        [Test]
        public void EnsurePlaylistDescriptionSize_WithMoreThan300CharacterDescriptionAndAtLeast2PeriodsWhereLastCharAllowedIsNotAPeriod_ShouldReturn() 
        {
            //Arrange
            string AiDescription = "Acoustic music offers listeners a number of different experiences and moods. Laid-back, peaceful music to up-tempo. This isn't really the best way to go about things you know. You could've at least tried to make something like a cake. Class clowns are pretty funny. Oh look, another sentence. Hi Mom. Did";
            string expectedDescription = "Acoustic music offers listeners a number of different experiences and moods. Laid-back, peaceful music to up-tempo. This isn't really the best way to go about things you know. You could've at least tried to make something like a cake. Class clowns are pretty funny. Oh look, another sentence. Hi Mom.";

            GeneratorsViewModel vm = new GeneratorsViewModel();
            vm.PlaylistDescription = AiDescription;

            //Act
            vm.PlaylistDescription = GeneratorsViewModel.EnsurePlaylistDescriptionSize(vm.PlaylistDescription);


            //Assert
            Assert.That(vm.PlaylistDescription.Equals(expectedDescription), Is.True);
        }

        [Test]
        public void EnsurePlaylistDescriptionSize_WithMoreThan300CharacterDescriptionAndAtLeast2PeriodsWherePeriodIsLastCharAllowed_ShouldReturnADescriptionThatEndsWithAPeriod()
        {
            //Arrange
            string AiDescription = "Acoustic music offers listeners a number of different experiences and moods. Laid-back, peaceful music to up-tempo. This isn't really the best way to go about things you know. You could've at least tried to make something like a cake. Class clowns are pretty funny. Oh look, another sentence. Hi Jim. Did";
            string expectedDescription = "Acoustic music offers listeners a number of different experiences and moods. Laid-back, peaceful music to up-tempo. This isn't really the best way to go about things you know. You could've at least tried to make something like a cake. Class clowns are pretty funny. Oh look, another sentence. Hi Jim.";

            GeneratorsViewModel vm = new GeneratorsViewModel();
            vm.PlaylistDescription = AiDescription;

            //Act
            vm.PlaylistDescription = GeneratorsViewModel.EnsurePlaylistDescriptionSize(vm.PlaylistDescription);

            Debug.WriteLine($"Length of playlist description: \n{vm.PlaylistDescription.Length}\n Description: \n {vm.PlaylistDescription}");

            //Assert
            Assert.That(vm.PlaylistDescription.Equals(expectedDescription), Is.True);
        }

        [Test]
        public void EnsurePlaylistDescriptionSize_WithMoreThan300CharacterDescriptionAndOnly1Period_ShouldReturnNull()
        {
            //Arrange
            string AiDescription = "Acoustic music offers listeners a number of different experiences and moods, laid-back, peaceful music to up-tempo, this isn't really the best way to go about things you know, you could've at least tried to make something like a cake, class clowns are pretty funny, oh look, another sentence, hi Jim, did";

            GeneratorsViewModel vm = new GeneratorsViewModel();
            vm.PlaylistDescription = AiDescription;
            
            //Act
            vm.PlaylistDescription = GeneratorsViewModel.EnsurePlaylistDescriptionSize(vm.PlaylistDescription);

            //Assert
            Assert.That(vm.PlaylistDescription == null);
        }
    }
}
