using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class TopArtistPageObject : PageObject
    {
        public TopArtistPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "TopArtist";
        }

        public IWebElement TopArtistIndexButton => _webDriver.FindElement(By.Id("topArtistGen"));
        public IWebElement GenerateButton => _webDriver.FindElement(By.Id("artist-generate"));
        public IWebElement track => _webDriver.FindElement(By.Id("remove-entry-icon-19"));
        public IWebElement NavbarToggle => _webDriver.FindElement(By.Id("navbar-toggle-button"));
        public IWebElement SpotifyLogin => _webDriver.FindElement(By.Id("spotify-button"));

        private IWebElement PlaylistNameInputLabel => _webDriver.FindElement(By.Id("titletext"));
        private IWebElement PlaylistNameInput => _webDriver.FindElement(By.Id("titleinput"));


        public void GoToTopArtistGenerator()
        {
            TopArtistIndexButton.Click();
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

        public bool PlaylistNameInputExists()
        {
            if ((PlaylistNameInput.Displayed == true) && (PlaylistNameInputLabel.Text.Contains("Playlist Title Input"))) 
            {
                return true;
            }
            return false;
        }

        public void FillInputForPlaylistName(string playlistName)
        {
            PlaylistNameInput.SendKeys(playlistName);
        }
    }
}
