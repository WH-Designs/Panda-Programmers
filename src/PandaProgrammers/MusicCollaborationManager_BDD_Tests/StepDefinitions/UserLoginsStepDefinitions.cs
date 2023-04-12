using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;

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

        [Given(@"the following users exist")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"the following users do not exist")]
        public void GivenTheFollowingUsersDoNotExist(Table table)
        {
            throw new PendingStepException();
        }

        [Given(@"I am a user with first name '([^']*)'")]
        public void GivenIAmAUserWithFirstName(string chad)
        {
            throw new PendingStepException();
        }

        [When(@"I login")]
        public void WhenILogin()
        {
            // Go to the login page
            _loginPage.GoTo();
            //Thread.Sleep(3000);
            // Now (attempt to) log them in.  Assumes previous steps defined the user
            TestUser u = (TestUser)_scenarioContext["CurrentUser"];
            _loginPage.EnterEmail(u.Email);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();
        }

        [Then(@"I am redirected to the '([^']*)' page")]
        public void ThenIAmRedirectedToThePage(string home)
        {
            throw new PendingStepException();
        }

        [Then(@"I can see the '([^']*)' Button")]
        public void ThenICanSeeTheButton(string button)
        {
            throw new PendingStepException();
        }

        [Given(@"I am a user with first name '([^']*)'")]
        public void GivenIAmAUserWithFirstName(string andre)
        {
            throw new PendingStepException();
        }

        [Then(@"I can see a login error message")]
        public void ThenICanSeeALoginErrorMessage()
        {
            throw new PendingStepException();
        }

        [Given(@"I am a user with first name '([^']*)'")]
        public void GivenIAmAUserWithFirstName(string talia)
        {
            throw new PendingStepException();
        }

        [Then(@"I can save cookies")]
        public void ThenICanSaveCookies()
        {
            throw new PendingStepException();
        }

        [Given(@"I am on the ""([^""]*)"" page")]
        public void GivenIAmOnThePage(string home)
        {
            throw new PendingStepException();
        }

        [When(@"I load previously saved cookies")]
        public void WhenILoadPreviouslySavedCookies()
        {
            throw new PendingStepException();
        }

        [When(@"I am on the ""([^""]*)"" page")]
        public void WhenIAmOnThePage(string home)
        {
            throw new PendingStepException();
        }

        [Then(@"I can see a personalized message in the navbar that includes my email")]
        public void ThenICanSeeAPersonalizedMessageInTheNavbarThatIncludesMyEmail()
        {
            throw new PendingStepException();
        }
    }
}
