using OpenQA.Selenium;
using SpecFlow.Actions.Selenium;
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
        private IWebElement ActiveTheme => _webDriver.FindElement(By.Id("main-primary-color-control"));

        private IWebElement FirstNameInput => _webDriver.FindElement(By.Id("FirstName"));
        private IWebElement LastNameInput => _webDriver.FindElement(By.Id("LastName"));

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

        public string ChangeSiteTheme() 
        {
            Random RandNum = new Random();
            int ThemeNum =  RandNum.Next(5);

            if (ThemeNum == 0) 
            {
                DefaultThemeBtn.Click();
                return "classicpanda";
            }
               
            else if(ThemeNum == 1) 
            {
                RedThemeBtn.Click();
                return "revolution";
            }

               
            else if (ThemeNum == 2) 
            {
                PurpleThemeBtn.Click();
                return "autumn";
            }
                
            else if (ThemeNum == 3) 
            {
                DarkThemeBtn.Click();
                return "moon";
            }
            else if (ThemeNum == 4) 
            {
                GoldThemeBtn.Click();
                return "luxury";
            }
            return "NO_THEME";
                
        }

        public bool IsFirstAndLastNameVisible(string firstName, string lastName) 
        {
            if (FirstNameInput.HasValue(firstName) == true
                 && LastNameInput.HasValue(lastName) == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsCurrentTheme(string tailwindNameOfTheme)
        {
            return ActiveTheme.HasClass(tailwindNameOfTheme);
        }
    }
}
