using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class SettingsPageObject: PageObject
    {
        public SettingsPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Settings";
        }



        private IWebElement DefaultThemeBtn => _webDriver.FindElement(By.Id("classicpanda-btn"));
        private IWebElement RedThemeBtn => _webDriver.FindElement(By.Id("revolution-btn"));
        private IWebElement PurpleThemeBtn => _webDriver.FindElement(By.Id("autumn-btn"));
        private IWebElement DarkThemeBtn => _webDriver.FindElement(By.Id("moon-btn"));
        private IWebElement GoldThemeBtn => _webDriver.FindElement(By.Id("mansion-btn"));

        //private IWebElement ThemesHeader => _webDriver.FindElement(By.);

        public bool ChangeThemeSectionIsVisible() 
        {

            if(DefaultThemeBtn.Text.Contains("Original") == true
                && RedThemeBtn.Text.Contains("Red") ==  true
                && PurpleThemeBtn.Text.Contains("Purple") == true
                && DarkThemeBtn.Text.Contains("Dark") == true
                && GoldThemeBtn.Text.Contains("Gold") == true
                ) 
            {
                return true;
            }
            else 
            {
                return false;
            }
        }
    }
}
