using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using Microsoft.AspDotNetCore.Mvc.RazorPages;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    public class TestUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    [Binding]
    public class UserLoginsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private IConfiguration Configuration { get; }

        public UserLoginsStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            
            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }

        [Given(@"the following users exist")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            IEnumerable<TestUser> users = table.CreateSet<TestUser>();
            _scenarioContext["Users"] = users;
        }

        [Given(@"the following users do not exist")]
        public void GivenTheFollowingUsersDoNotExist(Table table)
        {
            IEnumerable<TestUser> nonUsers = table.CreateSet<TestUser>();
            _scenarioContext["NonUsers"] = nonUsers;
         }

        [Given(@"I am a user with first name '([^']*)'"), When(@"I am a user with first name '([^']*)'")]
        public void GivenIAmAUserWithFirstName(string firstName)
        {
            IEnumerable<TestUser> users = (IEnumerable<TestUser>)_scenarioContext["Users"];
            TestUser u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            if (u == null)
            {
                // must have been selecting from non-users
                IEnumerable<TestUser> nonUsers = (IEnumerable<TestUser>)_scenarioContext["NonUsers"];
                u = nonUsers.Where(u => u.FirstName == firstName).FirstOrDefault();
            }
            _scenarioContext["CurrentUser"] = u;
        }

        [When(@"I login")]
        public void WhenILogin()
        {
            _loginPage.GoTo();
            TestUser u = (TestUser)_scenarioContext["CurrentUser"];
            _loginPage.EnterEmail(u.Email);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();
        }

        [Then(@"I am redirected to the '([^']*)' page")]
        public void ThenIAmRedirectedToThePage(string pageName)
        {
            _loginPage.GetURL().Should().Be(Common.UrlFor(pageName));
        }

        [Then(@"I can see the '([^']*)' Button")]
        public void ThenICanSeeTheButton(string button)
        {
            _homePage.DashboardAnchor.Should().NotBeNull();
            _homePage.DashboardAnchor.Displayed.Should().BeTrue();
        }

        [Then(@"I can see a login error message")]
        public void ThenICanSeeALoginErrorMessage()
        {
            _loginPage.HasLoginErrors().Should().BeTrue();
        }

        [Given(@"I am a visitor")]
        public void GivenIAmAVisitor()
        {
            //
        }

        [Then(@"I can see a link that contains '([^']*)'")]
        public void ThenICanSeeALinkThatContains(string text)
        {
            _loginPage.ResendEmailLink.Should().NotBeNull();
            _loginPage.ResendEmailLink.Displayed.Should().BeTrue();
            _loginPage.EmailLinkHasText(text).Should().BeTrue();
        }

        [When(@"I am on '([^']*)' page")]
        public void WhenIAmOnPage(string page)
        {
            _loginPage.GoTo();
        }


        [Then(@"I can save cookies")]
        public void ThenICanSaveCookies()
        {
            _homePage.SaveAllCookies().Should().BeTrue();
        }

        [Given(@"I am on the ""([^""]*)"" page")]
        public void GivenIAmOnThePage(string home)
        {
            _homePage.GoTo(home);
        }

        [When(@"I load previously saved cookies")]
        public void WhenILoadPreviouslySavedCookies()
        {
            _homePage.LoadAllCookies().Should().BeTrue();
        }

        [When(@"I am on the ""([^""]*)"" page")]
        public void WhenIAmOnThePage(string home)
        {
            _homePage.GoTo(home);
        }

        [Then(@"I can see a '([^']*)' button on the navbar")]
        public void ThenICanSeeAButtonOnTheNavbar(string button)
        {
            _homePage.SpotifyLoginButton.Should().NotBeNull();
            _homePage.SpotifyLoginButton.Displayed.Should().BeTrue();
        }

    }
}
