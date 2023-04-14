using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class SettingsPageObject: PageObject
    {
        public SettingsPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "Settings";
        }

        public bool ChangeThemeSectionIsVisible() 
        {
            return false;
        }
    }
}
