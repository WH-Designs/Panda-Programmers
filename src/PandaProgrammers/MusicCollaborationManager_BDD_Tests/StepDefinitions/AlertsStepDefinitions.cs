using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using FluentAssertions;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
	
namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    [Binding]
    public class AlertsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly HomePageObject _homePage;

        public AlertsStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _homePage = new HomePageObject(browserDriver.Current);
        }
        
        [When(@"I click on the dashboard button")]
        public void WhenIclickonthedashboardbutton()
        {
            _homePage.ShowNavbar();
            _homePage.GoToDashboard();
        }
        
        [Then(@"I will see an alert message")]
        public void ThenIwillseeanalertmessage()
        {
            _homePage.Alert.Should().NotBeNull();
        }


        [When(@"I click on the search button")]
        public void WhenIclickonthesearchbutton()
        {
            _homePage.ShowNavbar();
            _homePage.GoToSearch();
        }

    }
}