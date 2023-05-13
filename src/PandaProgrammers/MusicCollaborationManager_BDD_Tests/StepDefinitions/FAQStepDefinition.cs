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
    public class FAQ
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
    public class FAQStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly GeneratorPageObject _generatorPage;
        private readonly TopArtistPageObject _topArtistPage;
        private readonly FAQPageObject _faqPage;
        public FAQStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _generatorPage = new GeneratorPageObject(browserDriver.Current);
            _topArtistPage = new TopArtistPageObject(browserDriver.Current);
            _faqPage = new FAQPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }
        private IConfiguration Configuration { get; }

        [When(@"I select the FAQ page")]
        public void WhenISelectTheFAQPage()
        {
            _faqPage.GoToFAQ();
        }

        [Then(@"I should should see FAQ information buttons")]
        public void ThenIShouldShouldSeeFAQInformationButtons()
        {
            _faqPage.GeneralBtn.Should().NotBeNull();
            _faqPage.GeneralBtn.Displayed.Should().BeTrue();
            _faqPage.QuestBtn.Should().NotBeNull();
            _faqPage.QuestBtn.Displayed.Should().BeTrue();
            _faqPage.MoodBtn.Should().NotBeNull();
            _faqPage.MoodBtn.Displayed.Should().BeTrue();
            _faqPage.RelateBtn.Should().NotBeNull();
            _faqPage.RelateBtn.Displayed.Should().BeTrue();
            _faqPage.TimeBtn.Should().NotBeNull();
            _faqPage.TimeBtn.Displayed.Should().BeTrue();
            _faqPage.TopABtn.Should().NotBeNull();
            _faqPage.TopABtn.Displayed.Should().BeTrue();
            _faqPage.TopTBtn.Should().NotBeNull();
            _faqPage.TopTBtn.Displayed.Should().BeTrue();
            _faqPage.InputBtn.Should().NotBeNull();
            _faqPage.InputBtn.Displayed.Should().BeTrue();
            _faqPage.AIBtn.Should().NotBeNull();
            _faqPage.AIBtn.Displayed.Should().BeTrue();
        }

        [When(@"I click any information button")]
        public void WhenIClickAnyInformationButton()
        {
            _faqPage.OpenAll();
        }

        [Then(@"I should should see FAQ information")]
        public void ThenIShouldShouldSeeFAQInformation()
        {
            _faqPage.GeneralContent.Should().NotBeNull();
            _faqPage.GeneralContent.Displayed.Should().BeTrue();
            _faqPage.QuestContent.Should().NotBeNull();
            _faqPage.QuestContent.Displayed.Should().BeTrue();
            _faqPage.MoodContent.Should().NotBeNull();
            _faqPage.MoodContent.Displayed.Should().BeTrue();
            _faqPage.RelateContent.Should().NotBeNull();
            _faqPage.RelateContent.Displayed.Should().BeTrue();
            _faqPage.TimeContent.Should().NotBeNull();
            _faqPage.TimeContent.Displayed.Should().BeTrue();
            _faqPage.TopAContent.Should().NotBeNull();
            _faqPage.TopAContent.Displayed.Should().BeTrue();
            _faqPage.TopTContent.Should().NotBeNull();
            _faqPage.TopTContent.Displayed.Should().BeTrue();
            _faqPage.InputContent.Should().NotBeNull();
            _faqPage.InputContent.Displayed.Should().BeTrue();
            _faqPage.AIContent.Should().NotBeNull();
            _faqPage.AIContent.Displayed.Should().BeTrue();
        }

        [Then(@"I should should see a tutotial video")]
        public void ThenIShouldShouldSeeATutotialVideo()
        {
            _faqPage.QuestVid.Should().NotBeNull();
            _faqPage.QuestVid.Displayed.Should().BeTrue();
            _faqPage.MoodVid.Should().NotBeNull();
            _faqPage.MoodVid.Displayed.Should().BeTrue();
            _faqPage.RelateVid.Should().NotBeNull();
            _faqPage.RelateVid.Displayed.Should().BeTrue();
            _faqPage.TimeVid.Should().NotBeNull();
            _faqPage.TimeVid.Displayed.Should().BeTrue();
            _faqPage.TopAVid.Should().NotBeNull();
            _faqPage.TopAVid.Displayed.Should().BeTrue();
            _faqPage.TopTVid.Should().NotBeNull();
            _faqPage.TopTVid.Displayed.Should().BeTrue();
            _faqPage.InputVid.Should().NotBeNull();
            _faqPage.InputVid.Displayed.Should().BeTrue();
            _faqPage.AIVid.Should().NotBeNull();
            _faqPage.AIVid.Displayed.Should().BeTrue();
        }


    }
}
