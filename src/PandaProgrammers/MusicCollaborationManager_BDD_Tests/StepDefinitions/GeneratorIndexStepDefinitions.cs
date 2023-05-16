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
    public class TestUserGenerator
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string descriptionKey { get; set; }
        public string titleKey { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }

    [Binding]
    public class GeneratorIndexStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly LoginPageObject _loginPage;
        private readonly HomePageObject _homePage;
        private readonly GeneratorPageObject _generatorPage;

        private IConfiguration Configuration { get; }

        public GeneratorIndexStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _loginPage = new LoginPageObject(browserDriver.Current);
            _homePage = new HomePageObject(browserDriver.Current);
            _generatorPage = new GeneratorPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<UserLoginsStepDefinitions>();
            Configuration = builder.Build();
        }

        [Given(@"the following users exist generator")]
        public void GivenTheFollowingUsersExist(Table table)
        {
            IEnumerable<TestUserGenerator> users = table.CreateSet<TestUserGenerator>();
            _scenarioContext["Users"] = users;
        }

        [Given(@"the following generator titles exist")]
        public void GivenTheFollowingGeneratorTitlesExist(Table table)
        {
            IEnumerable<TestUserGenerator> titles = table.CreateSet<TestUserGenerator>();
            _scenarioContext["Titles"] = titles;
        }

        [Given(@"the following generartor descriptions exist")]
        public void GivenTheFollowingGenerartorDescriptionsExist(Table table)
        {
            IEnumerable<TestUserGenerator> descr = table.CreateSet<TestUserGenerator>();
            _scenarioContext["Description"] = descr;
        }

        [Given(@"I am a logged in user with first name '([^']*)'")]
        public void GivenIAmALoggedInUserWithFirstName(string firstName)
        {
            IEnumerable<TestUserGenerator> users = (IEnumerable<TestUserGenerator>)_scenarioContext["Users"];
            TestUserGenerator u = users.Where(u => u.FirstName == firstName).FirstOrDefault();
            _scenarioContext["CurrentUser"] = u;

            _loginPage.GoTo();
            _loginPage.EnterEmail(u.Email);
            _loginPage.EnterPassword(u.Password);
            _loginPage.Login();

        }

        [When(@"I click the generator button in the navbar")]
        public void WhenIClickTheGeneratorButtonInTheNavbar()
        {
            _generatorPage.GoToGeneratorIndex();
        }

        [Then(@"I should be redirected to the '([^']*)' page")]
        public void ThenIShouldBeRedirectedToThePage(string pageName)
        {
            _generatorPage.GetURL().Should().Be(Common.UrlFor(pageName));
        }

        [When(@"I am on the '([^']*)' page")]
        public void WhenIAmOnThePage(string page)
        {
            _generatorPage.GoTo();
        }

        [Given(@"I am on the '([^']*)' page")]
        public void GivenIAmOnThePage(string page)
        {
            _generatorPage.GoTo();
        }

        [When(@"I click on one of the generator buttons")]
        public void WhenIClickOnOneOfTheGeneratorButtons()
        {
            _generatorPage.GoToGenerator();
        }

        [Then(@"I should be navigated to that generators '([^']*)' page")]
        public void ThenIShouldBeNavigatedToThatGeneratorsPage(string pageName)
        {
            _generatorPage.GetURL().Should().Be(Common.UrlFor(pageName));
        }


        [Then(@"I should see a title of a generator")]
        public void ThenIShouldSeeATitleOfAGenerator()
        {
            _generatorPage.Header.Should().NotBeNull();
            _generatorPage.Header.Displayed.Should().BeTrue();
            _generatorPage.HeaderHasText("Questionnaire Generator")
                .Should().BeTrue();
        }

        [Then(@"I should see a '([^']*)' description of a generator")]
        public void ThenIShouldSeeADescriptionOfAGenerator(string descriptionKey)
        {
            IEnumerable<TestUserGenerator> desc = (IEnumerable<TestUserGenerator>)_scenarioContext["Description"];
            TestUserGenerator u = desc.Where(u => u.descriptionKey == descriptionKey).FirstOrDefault();

            _generatorPage.Description.Should().NotBeNull();
            _generatorPage.Description.Displayed.Should().BeTrue();
            _generatorPage.DescriptionHasText(u.Description)
                .Should().BeTrue();
        }

        [Then(@"I should see a '([^']*)' title of a generator")]
        public void ThenIShouldSeeATitleOfAGenerator(string titleKey)
        {
            IEnumerable<TestUserGenerator> title = (IEnumerable<TestUserGenerator>)_scenarioContext["Titles"];
            TestUserGenerator u = title.Where(u => u.titleKey == titleKey).FirstOrDefault();

            _generatorPage.Header.Should().NotBeNull();
            _generatorPage.Header.Displayed.Should().BeTrue();
            _generatorPage.HeaderHasText(u.Title)
                .Should().BeTrue();
        }



    }
}
