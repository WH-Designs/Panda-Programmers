using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    public class TestUserSettings
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    [Binding]
    public class PresetThemesStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly SettingsPageObject _settingsPage;

        private IConfiguration Configuration { get; }

        public PresetThemesStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _settingsPage = new SettingsPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }

        [Given(@"the following users exist settings")]
        public void GivenTheFollowingUsersExistSettings(Table table)
        {
            IEnumerable<TestUserSettings> users = table.CreateSet<TestUserSettings>();
            _scenarioContext["Users"] = users;
        }

        [Given(@"I am a logged in user")]
        public void GivenIAmALoggedInUser()
        {
            _loginPage.GoTo();
            _loginPage.EnterEmail("chadb@gmail.com");
            _loginPage.EnterPassword("Pass321!");
            _loginPage.Login();
        }

        [When(@"I click on the settings button in the navbar")]
        public void WhenIClickOnTheSettingsButtonInTheNavbar()
        {
            _settingsPage.GoTo();
        }

        [Then(@"I should be taken to the settings page")]
        public void ThenIShouldBeTakenToTheSettingsPage()
        {
            _settingsPage.GetURL().Should().Be(Common.UrlFor("Settings"));
        }

        //----Feature test starts (below)------------

        //-------------

        [Given(@"I am a listener")]
        public void GivenIAmAListener()
        {
            _loginPage.GoTo();
            _loginPage.EnterEmail("chadb@gmail.com");
            _loginPage.EnterPassword("Pass321!");
            _loginPage.Login();
        }

        [When(@"I am on my profile page")]
        public void WhenIAmOnMyProfilePage()
        {
            _settingsPage.GoTo();
        }

        [Then(@"I can see the section with options to change my theme to different pre-made themes")]
        public void ThenICanSeeTheSectionWithOptionsToChangeMyThemeToDifferentPre_MadeThemes()
        {
            _settingsPage.ChangeThemeSectionIsVisible();
        }

        //------------

        //[Given(@"I am a listener on my profile page")]
        //public void GivenIAmAListenerOnMyProfilePage()
        //{
        //    throw new PendingStepException();
        //}

        //[When(@"I click on one of the different pre-made theme buttons")]
        //public void WhenIClickOnOneOfTheDifferentPre_MadeThemeButtons()
        //{
        //    throw new PendingStepException();
        //}

        //[Then(@"the website responds accordingly and adjusts my theme without a page reload")]
        //public void ThenTheWebsiteRespondsAccordinglyAndAdjustsMyThemeWithoutAPageReload()
        //{
        //    throw new PendingStepException();
        //}


    }
}
