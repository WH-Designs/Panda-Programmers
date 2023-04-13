﻿using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using MusicCollaborationManager_BDD_Tests.Shared;
using System.Collections.ObjectModel;

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

        public void Logout()
        {
            IWebElement navbarLogoutButton = _webDriver.FindElement(By.Id("logout-button"));
            navbarLogoutButton.Click();
        }
    }
}
