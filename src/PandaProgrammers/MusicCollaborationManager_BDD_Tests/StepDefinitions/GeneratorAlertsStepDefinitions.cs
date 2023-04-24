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
    [Binding]
    public class GeneratorAlertsStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly GeneratorPageObject _generatorPage;

        public GeneratorAlertsStepDefinitions(ScenarioContext scenarioContext, BrowserDriver browserDriver)
        {
            _scenarioContext = scenarioContext;
            _generatorPage = new GeneratorPageObject(browserDriver.Current);
        }
			
        [When(@"I click on the generate playlist button")]
        public void WhenIclickonthegenerateplaylistbutton()
        {
            _generatorPage.GeneratePlaylist();
        }
    }
}