using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using FluentAssertions;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    public class TestArtistGenerator
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    [Binding]
    public class ArtistGeneratorStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly GeneratorPageObject _generatorPage;
        private readonly TopArtistPageObject _topArtistPage;
        public ArtistGeneratorStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _generatorPage = new GeneratorPageObject(browserDriver.Current);
            _topArtistPage = new TopArtistPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }
        private IConfiguration Configuration { get; }

        [When(@"I select the top track artist page")]
        public void WhenISelectTheTopTrackArtistPage()
        {
            _topArtistPage.GoToTopArtistGenerator();
        }

        [When(@"I go to the top artist playlist page")]
        public void WhenIGoToTheTopArtistPlaylistPage()
        {
            _topArtistPage.GoTo();
        }

        [Then(@"I should be able to select a button to generate tracks automatically with no input")]
        public void ThenIShouldBeAbleToSelectAButtonToGenerateTracksAutomaticallyWithNoInput()
        {
            _topArtistPage.GenerateButton.Should().NotBeNull();
            _topArtistPage.GenerateButton.Displayed.Should().BeTrue();
        }

        [Given(@"I am a listener on the top artist playlist page")]
        public void GivenIAmAListenerOnTheTopArtistPlaylistPage()
        {
            _topArtistPage.CheckLogin();
            _topArtistPage.GoTo();
        }

        [When(@"I select generate playlist")]
        public void WhenISelectGeneratePlaylist()
        {
            _topArtistPage.GeneratePlaylist();
        }

        [Then(@"I should get a playlist that has similar tracks to my top artists")]
        public void ThenIShouldGetAPlaylistThatHasSimilarTracksToMyTopArtists()
        {
            _topArtistPage.track.Should().NotBeNull();
            _topArtistPage.track.Displayed.Should().BeTrue();
        }

        [Then(@"I should see a spot to enter a playlist name")]
        public void ThenIShouldSeeASpotToEnterAPlaylistName()
        {
            _topArtistPage.PlaylistNameInputExists().Should().BeTrue();
        }

        [Then(@"I should see error messages about what input is invalid")]
        public void ThenIShouldSeeErrorMessagesAboutWhatInputIsInvalid()
        {
            _topArtistPage.InvalidInputMessagesAreVisible().Should().BeTrue();
        }

        [Then(@"I should see a playlist visibility setting")]
        public void ThenIShouldSeeAPlaylistVisibilitySetting()
        {
            _topArtistPage.PlaylistVisibilityOptionExists().Should().BeTrue();
        }


    }
}
