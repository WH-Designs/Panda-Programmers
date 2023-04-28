using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    [Binding]
    public class GeneratorRelatedArtistsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly GeneratorPageObject _generatorPage;
        private readonly RelatedGeneratorPageObject _relatedArtistPage;
        public GeneratorRelatedArtistsStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _generatorPage = new GeneratorPageObject(browserDriver.Current);
            _relatedArtistPage = new RelatedGeneratorPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }
        private IConfiguration Configuration { get; }

        [When(@"I go to the related artist playlist page")]
        public void WhenIGoToTheRelatedArtistPlaylistPage()
        {
            _relatedArtistPage.GoTo();
        }

        [Then(@"I should see a select artist option")]
        public void ThenIShouldSeeASelectArtistOption()
        {
            _relatedArtistPage.ArtistSelectList.Should().NotBeNull();
            _relatedArtistPage.ArtistSelectList.Displayed.Should().BeTrue();
        }

        [Given(@"I am a listener on the related artist playlist page")]
        public void GivenIAmAListenerOnTheRelatedArtistPlaylistPage()
        {
            _relatedArtistPage.CheckLogin();
            _relatedArtistPage.GoTo();
        }

        [Then(@"I should get a playlist that has similar tracks to my selected artists")]
        public void ThenIShouldGetAPlaylistThatHasSimilarTracksToMySelectedArtists()
        {
            _relatedArtistPage.track.Should().NotBeNull();
            _relatedArtistPage.track.Displayed.Should().BeTrue();
        }

        [When(@"I select the related artist page")]
        public void WhenISelectTheRelatedArtistPage()
        {
            _relatedArtistPage.GoToRelatedArtistGenerator();
        }

        [Then(@"I should be redirected to the related artists page")]
        public void ThenIShouldBeRedirectedToTheRelatedArtistsPage()
        {
            _relatedArtistPage.GoToRelatedArtistGenerator();  
        }

        [When(@"I select generate the playlist")]
        public void WhenISelectGenerateThePlaylist()
        {
            _relatedArtistPage.GeneratePlaylist();
        }

    }
}
