using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    [Binding]
    public class UserResendingEmailConfirmationStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly ResendEmailPageObject _resendEmailPage;

        private IConfiguration Configuration { get; }

        public UserResendingEmailConfirmationStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _loginPage = new LoginPageObject(browserDriver.Current); 
            _resendEmailPage = new ResendEmailPageObject(browserDriver.Current); ;
            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }

        [When(@"I click on the resend email confirmation link")]
        public void WhenIClickOnTheResendEmailConfirmationLink()
        {
            _loginPage.ResendEmailPage();
        }

        [Then(@"I am redirected to the resend email confirmation page")]
        public void ThenIAmRedirectedToTheResendEmailConfirmationPage()
        {
            _resendEmailPage.GetURL().Should().Be(Common.UrlFor("EmailConfirmation"));
        }

        [When(@"I am on the Resend Email Confirmation page")]
        public void WhenIAmOnTheResendEmailConfirmationPage()
        {
            _resendEmailPage.GoTo();
        }

        [Then(@"I can see an email input")]
        public void ThenICanSeeAnEmailInput()
        {
            _resendEmailPage.EmailInput.Should().NotBeNull();
            _resendEmailPage.EmailInput.Displayed.Should().BeTrue();
        }
    }
}
