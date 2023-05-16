using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class TopArtistPostPageObject : PageObject
    {
        public TopArtistPostPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "TopArtistPost";
        }

        //public IWebElement EmailInput => _webDriver.FindElement(By.Id("Input_Email"));
        //public IWebElement PasswordInput => _webDriver.FindElement(By.Id("Input_Password"));
        //public IWebElement RememberMeCheck => _webDriver.FindElement(By.Id("Input_RememberMe"));
        //public IWebElement SubmitButton => _webDriver.FindElement(By.Id("login-submit"));
        //public IWebElement NavButton => _webDriver.FindElement(By.Id("generator-anchor"));
        //public IWebElement Header => _webDriver.FindElement(By.Id("question-header"));
        //public IWebElement Description => _webDriver.FindElement(By.Id("question-descr"));
        //public IWebElement GeneratorButton => _webDriver.FindElement(By.Id("mood-anchor"));
        //public IWebElement NavbarToggle => _webDriver.FindElement(By.Id("navbar-toggle-button"));
        //public IWebElement GenerateButton => _webDriver.FindElement(By.Id("playlist-generate-button"));

        private IWebElement PlaylistTitle => _webDriver.FindElement(By.Id("playlistTitle"));

        public bool IsCurrentPlaylistTitle(string playlistTitle) 
        {
            return PlaylistTitle.Text.Contains(playlistTitle);
        }

    }
}
