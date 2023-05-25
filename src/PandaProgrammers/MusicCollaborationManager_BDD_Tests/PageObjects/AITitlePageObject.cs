using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class AITitlePageObject : PageObject
    {
        public AITitlePageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "AITitle";
        }

        public IWebElement AISwitch => _webDriver.FindElement(By.Id("aiTitle"));
        public IWebElement ManualTitle => _webDriver.FindElement(By.Id("titleinput"));
        public IWebElement OnSwitch => _webDriver.FindElement(By.Id("offbtn"));
        public IWebElement OffSwitch => _webDriver.FindElement(By.Id("onbtn"));
        public IWebElement OutputTitle => _webDriver.FindElement(By.Id("playlistTitle"));
        public IWebElement TrackInputPage => _webDriver.FindElement(By.Id("trackInput"));
        public IWebElement GenerateBtn => _webDriver.FindElement(By.Id("inputTrack"));

        public void SwitchOn()
        {
            OnSwitch.Click();
        }

        public void SwitchOff()
        {
            OffSwitch.Click();
        }

        public void GoToTrackInputPage()
        {
            TrackInputPage.Click();
        }

        public void GenerateInputBtn()
        {
            GenerateBtn.Click();
            Thread.Sleep(10000);
        }
    }
}

