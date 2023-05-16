using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class ResendEmailPageObject : PageObject
    {
        public ResendEmailPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "EmailConfirmation";
        }

        public IWebElement EmailInput => _webDriver.FindElement(By.Id("email-input"));
    }
}
