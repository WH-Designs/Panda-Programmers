using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MusicCollaborationManager_BDD_Tests.Shared;
using System.Collections.ObjectModel;
using SpecFlow.Actions.Selenium;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class HomePageObject : PageObject
    {
        public HomePageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Home";
        }

        public IWebElement RegisterButton => _webDriver.FindElement(By.Id("register-link"));
        public IWebElement DashboardAnchor => _webDriver.FindElement(By.Id("dashboard-anchor"));
        public IWebElement SearchAnchor => _webDriver.FindElement(By.Id("search-anchor"));
        public IWebElement navbarLogoutButton => _webDriver.FindElement(By.Id("logout-button"));
        public IWebElement SettingsAnchor => _webDriver.FindElement(By.CssSelector("a[href=\"/Listener/Settings\"]"));
        public IWebElement SpotifyLoginButton => _webDriver.FindElement(By.Id("spotify-button"));
        public IWebElement NavbarToggleButton => _webDriver.FindElement(By.Id("navbar-toggle-button"));
        private IWebElement YouTubeTopMusicVideosHeader => _webDriver.FindElement(By.XPath("/html/body/div[2]/main/div/div[5]/div//h2"));
        private IWebElement YouTubeIcon => _webDriver.FindElement(By.XPath("/html/body/div[2]/main/div/div[5]/div//img"));
        public IWebElement Alert => _webDriver.FindElement(By.Id("generator-alert"));

        public void GoToSettings() 
        {
            SettingsAnchor.Click();
        }
        public void GoToSearch() 
        {
            SearchAnchor.Click();
        }

        public void GoToDashboard()
        {
            DashboardAnchor.Click();
        }
        public void Logout()
        {
            navbarLogoutButton.Click();
        }

        public void ShowNavbar()
        {
            NavbarToggleButton.Click();
        }

        public bool YouTubeMusicVideosSectionExists()
        {
            if (YouTubeTopMusicVideosHeader.Text.Contains("Top music videos") && (YouTubeIcon.GetAttribute("alt") == "YouTube Icon"))
            {
                return true;
            }
            return false;
        }
    }
}
