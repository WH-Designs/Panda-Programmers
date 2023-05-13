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
    public class AITitle
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
    public class AITitleStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly GeneratorPageObject _generatorPage;
        private readonly TopArtistPageObject _topArtistPage;
        private readonly FAQPageObject _faqPage;
        private readonly AITitlePageObject _aiTitle;
        
        public AITitleStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _generatorPage = new GeneratorPageObject(browserDriver.Current);
            _topArtistPage = new TopArtistPageObject(browserDriver.Current);
            _faqPage = new FAQPageObject(browserDriver.Current);
            _aiTitle = new AITitlePageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }
        private IConfiguration Configuration { get; }

        [When(@"I go to a playlist page")]
        public void WhenIGoToAPlaylistPage()
        {
            _topArtistPage.GoToTopArtistGenerator();
        }

        [Then(@"I should see an option to switch to an AI generated title")]
        public void ThenIShouldSeeAnOptionToSwitchToAnAIGeneratedTitle()
        {
            _aiTitle.AISwitch.Should().NotBeNull();
            _aiTitle.AISwitch.Displayed.Should().BeTrue();
        }

        [When(@"when I select and unselect the AI option switch")]
        public void WhenWhenISelectAndUnselectTheAIOptionSwitch()
        {
            _aiTitle.SwitchOn();
            _aiTitle.SwitchOff();
        }

        [Then(@"I the manual title entry should not be hidden")]
        public void ThenITheManualTitleEntryShouldNotBeHidden()
        {
            _aiTitle.ManualTitle.Should().NotBeNull();
            _aiTitle.ManualTitle.Displayed.Should().BeTrue();
        }

        [When(@"when I select the on AI switch option")]
        public void WhenWhenISelectTheOnAISwitchOption()
        {
            _aiTitle.SwitchOn();
        }

        [Then(@"I should see a generated title on the generator output page")]
        public async void ThenIShouldSeeAGeneratedTitleOnTheGeneratorOutputPage()
        {

            _aiTitle.OutputTitle.Should().NotBeNull();
            _aiTitle.OutputTitle.Displayed.Should().BeTrue();
        }

        [Given(@"I am logged into Spotify")]
        public void GivenIAmLoggedIntoSpotify()
        {
            _topArtistPage.CheckLogin();
        }

        [When(@"I go to the track input generator page")]
        public void WhenIGoToTheTrackInputGeneratorPage()
        {
            _aiTitle.GoToTrackInputPage();
        }

        [When(@"I select generate playlist without selecting an AI title switch")]
        public void WhenISelectGeneratePlaylistWithoutSelectingAnAITitleSwitch()
        {
            _aiTitle.GenerateInputBtn();
        }


    }
}
