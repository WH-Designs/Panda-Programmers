using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using Microsoft.Extensions.Configuration;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using Microsoft.Extensions.Configuration;
using System;
using TechTalk.SpecFlow;
using FluentAssertions;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    [Binding]
    public class CreatePlaylistNamePostStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly GeneratorPageObject _generatorPage;
        private readonly TopArtistPageObject _topArtistPage;
        private readonly FAQPageObject _faqPage;
        private readonly AITitlePageObject _aiTitle;
        private readonly TopArtistPostPageObject _topArtistPost;

        private IConfiguration Configuration { get; }

        public CreatePlaylistNamePostStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _generatorPage = new GeneratorPageObject(browserDriver.Current);
            _topArtistPage = new TopArtistPageObject(browserDriver.Current);
            _faqPage = new FAQPageObject(browserDriver.Current);
            _aiTitle = new AITitlePageObject(browserDriver.Current);
            _topArtistPost = new TopArtistPostPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }

        [When(@"I type '([^']*)' as the playlist name")]
        public void WhenITypeAsThePlaylistName(string catchySongs)
        {
            _topArtistPage.FillInputForPlaylistName(catchySongs);
        }

        [Then(@"I should see '([^']*)' as the playlist title on the generator output page")]
        public void ThenIShouldSeeAsThePlaylistTitleOnTheGeneratorOutputPage(string playlistName)
        {
            _topArtistPost.IsCurrentPlaylistTitle(playlistName);
        }


        [When(@"I click on the button to generate the playlist")]
        public void WhenIClickOnTheButtonToGenerateThePlaylist()
        {
            _topArtistPage.GeneratePlaylist();
        }

        [Then(@"I should see a playlist visibility setting")]
        public void ThenIShouldSeeAPlaylistVisibilitySetting()
        {
            _topArtistPost.PlaylistVisibilityOptionExists().Should().BeTrue();
        }

    }
}
