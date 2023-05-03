using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class RelatedGeneratorPageObject : PageObject
    {
        public RelatedGeneratorPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "RelatedGenerator";
        }

        public IWebElement RelatedArtistIndexButton => _webDriver.FindElement(By.Id("relatedArtistGen"));
        public IWebElement GenerateButton => _webDriver.FindElement(By.Id("related-generate-button"));
        public IWebElement NavbarToggle => _webDriver.FindElement(By.Id("navbar-toggle-button"));
        public IWebElement SpotifyLogin => _webDriver.FindElement(By.Id("spotify-button"));
        public IWebElement ArtistSelectList => _webDriver.FindElement(By.Id("artist"));
        public IWebElement track => _webDriver.FindElement(By.Id("remove-entry-icon-19"));

        public void GoToRelatedArtistGenerator()
        {
            RelatedArtistIndexButton.Click();
        }

        public void GeneratePlaylist()
        {
            GenerateButton.Click();
        }

        public void CheckLogin()
        {
            NavbarToggle.Click();
            SpotifyLogin.Click();
        }
    }
}
