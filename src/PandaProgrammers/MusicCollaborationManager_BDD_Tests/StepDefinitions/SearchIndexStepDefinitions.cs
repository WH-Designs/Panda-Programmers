using MusicCollaborationManager_BDD_Tests.Drivers;
using MusicCollaborationManager_BDD_Tests.PageObjects;
using MusicCollaborationManager_BDD_Tests.Shared;
using System;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Microsoft.Extensions.Configuration;
using System.Diagnostics;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MusicCollaborationManager_BDD_Tests.StepDefinitions
{
    public class SearchTestUser
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }

    [Binding]
    public class SearchStepDefinitions
    {
        private readonly ScenarioContext _scenarioContext;
        private readonly SearchPageObject _searchPage;
        private readonly LoginPageObject _loginPage;
        private IConfiguration Configuration { get; }

        public SearchStepDefinitions(ScenarioContext context, BrowserDriver browserDriver)
        {
            _scenarioContext = context;
            _searchPage = new SearchPageObject(browserDriver.Current);
            _loginPage = new LoginPageObject(browserDriver.Current);

            IConfigurationBuilder builder = new ConfigurationBuilder().AddUserSecrets<SearchStepDefinitions>();
            Configuration = builder.Build();
        }

        [Given(@"the following users exist search")]
        public void GivenTheFollowingUsersExistSearch(Table table)
        {
            IEnumerable<SearchTestUser> users = table.CreateSet<SearchTestUser>();
            _scenarioContext["Users"] = users;
        }

        [Given(@"I am a listener on the search page")]
        public void GivenIAmAListenerOnTheSearchPage()
        {
            _loginPage.GoTo();
            _loginPage.EnterEmail("chadb@gmail.com");
            _loginPage.EnterPassword("Pass321!");
            _loginPage.Login();

            _searchPage.GoToSearchIndex();
        }

        [When(@"I click the search button in the navbar")]
        public void WhenIClickTheSearchButtonInTheNavbar()
        {
            _searchPage.GoToSearchIndex();
        }


        [Given(@"I type a query into the search bar")]
        public void GivenITypeAQueryIntoTheSearchBar()
        {
            _searchPage.InputSearch("Doja Cat");
        }

        [When(@"I click the search button")]
        public void WhenIclickthesearchbutton()
        {
            _searchPage.ClickSpotifyRadioButton();
            _searchPage.ClickAllRadioButton();
            _searchPage.SendSearch();
        }

        [Then(@"I can see the search results")]
        public void ThenIcanseethesearchresults()
        {
            // wait for a few 
            _searchPage.SearchResultsPointer.Displayed.Should().BeTrue();
        }

        [When(@"I type an empty query into the search bar")]
        public void WhenItypeanemptyqueryintothesearchbar()
        {
            _searchPage.InputSearch("");
            _searchPage.SendSearch();
        }

        [Then(@"I should not see any search results")]
        public void ThenIShouldNotSeeAnySearchResults()
        {
            _searchPage.SearchHeaderHasText("Showing results for: ");
        }


        [When(@"I submit a query that has no results")]
        public void WhenIsubmitaquerythathasnoresults()
        {
            _searchPage.InputSearch("NDUISAHLgdsauiofhilcasFUDISALHFIASPDJFHIVfdsaghdgdfsNPIASJIOK");
            _searchPage.SendSearch();
        }

        [Then(@"I should see a message indicating that there are no results")]
        public void ThenIshouldseeamessageindicatingthattherearenoresults()
        {
             _searchPage.SearchRowNoResults.Displayed.Should().BeTrue();
        }

    }
}