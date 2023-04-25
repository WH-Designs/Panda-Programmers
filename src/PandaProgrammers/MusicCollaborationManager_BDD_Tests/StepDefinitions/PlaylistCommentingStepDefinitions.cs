using FluentAssertions;
using Microsoft.Extensions.Configuration;
using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    [Binding]
    public class PlaylistCommentingStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly PlaylistPageObject _playlistPage;
        private readonly HomePageObject _homePage;

        private IConfiguration Configuration { get; }

        public PlaylistCommentingStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _playlistPage = new PlaylistPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<PlaylistCommentingStepDefinitions>();
            Configuration = builder.Build();
        }

        [When(@"I am on the playlist page")]
        public void WhenIAmOnThePlaylistPage()
        {
            _playlistPage.GoTo();
        }

        [Then(@"I can see the comment form on the page")]
        public void ThenICanSeeTheCommentFormOnThePage()
        {
            _playlistPage.CommentForm.Should().NotBeNull();
        }

        [When(@"I input '([^']*)' into the form")]
        public void WhenIInputIntoTheForm(string text)
        {
            _playlistPage.EnterComment(text);
        }

        [When(@"I click the submit button")]
        public void WhenIClickTheSubmitButton()
        {
            _playlistPage.SubmitComment();
        }

        [Then(@"I should see '([^']*)' on the page")]
        public void ThenIShouldSeeOnThePage(string text)
        {
            throw new PendingStepException();
        }

        [Then(@"I should see submit error message")]
        public void ThenIShouldSeeSubmitErrorMessage()
        {
            throw new PendingStepException();
        }

        [Given(@"I am on the playlist page")]
        public void GivenIAmOnThePlaylistPage()
        {
            _playlistPage.GoTo();
        }

    }
}
