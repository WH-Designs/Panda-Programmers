using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MusicCollaborationManager_BDD_Tests.Shared;
using System.Collections.ObjectModel;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class SearchPageObject : PageObject
    {
        public SearchPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Search";
        }

        public IWebElement SearchBar => _webDriver.FindElement(By.Id("spotify-search"));
        public IWebElement AllRadioButton => _webDriver.FindElement(By.Id("checkbox-item-1"));
        public IWebElement SpotifyRadioButton => _webDriver.FindElement(By.Id("spotify-radio"));
        public IWebElement SearchButton => _webDriver.FindElement(By.Id("search-button"));
        public IWebElement NavbarToggle => _webDriver.FindElement(By.Id("navbar-toggle-button"));
        public IWebElement NavButton => _webDriver.FindElement(By.Id("search-anchor"));
        public IWebElement SearchResultsPointer => _webDriver.FindElement(By.Id("query-display"));
        public IWebElement SearchRowNoResults => _webDriver.FindElement(By.Id("search-row-no-results"));

        public void GoToSearchIndex()
        {
            NavbarToggle.Click();
            NavButton.Click();
        }

        public void InputSearch(string search) 
        {
            SearchBar.Clear();
            SearchBar.SendKeys(search);
        }
        public void SendSearch()
        {
            SearchButton.Click();
        }
        public void ClickSpotifyRadioButton()
        {
            SpotifyRadioButton.Click();
        }
        public void ClickAllRadioButton()
        {
            AllRadioButton.Click();
        }
        public bool SearchHeaderHasText(string text)
        {
            return !SearchResultsPointer.Text.Contains(text);
        }
    }
}
