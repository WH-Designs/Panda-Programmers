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

        [Given(@"I am a user logged in with the username '([^']*)'")]
        public void GivenIAmAUserLoggedInWithTheUsername(string firstName)
        {
            IEnumerable<TestUserSettings> users = (IEnumerable<TestUserSettings>)_scenarioContext["Users"];
            TestUserSettings u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            _scenarioContext["CurrentUser"] = u;

            _loginPage.GoTo();
            _loginPage.EnterEmail(u.Email);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();
        }

        [When(@"I click on the settings button in the navbar")]
        public void WhenIClickOnTheSettingsButtonInTheNavbar()
        {
            _homePage.GoToSettings();
        }

        [Then(@"I should be taken to the '([^']*)' page")]
        public void ThenIShouldBeTakenToThePage(string pageName)
        {
            _settingsPage.GetURL().Should().Be(Common.UrlFor(pageName));
        }
    }
}
