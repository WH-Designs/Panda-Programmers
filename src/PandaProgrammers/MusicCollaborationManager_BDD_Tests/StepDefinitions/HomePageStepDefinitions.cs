using FluentAssertions;
using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using System;
using TechTalk.SpecFlow;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    [Binding]
    public class HomePageStepDefinitions
    {
        private readonly HomePageObject _homePage;

        public HomePageStepDefinitions(BrowserDriver browserDriver) 
        {
            _homePage = new HomePageObject(browserDriver.Current);
        }

        [Then(@"I should the top music videos from YouTube")]
        public void ThenIShouldTheTopMusicVideosFromYouTube()
        {
           _homePage.YouTubeMusicVideosSectionExists().Should().BeTrue();
        }
    }
}
