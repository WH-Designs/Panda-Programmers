using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class GeneratorPageObject : PageObject
    {
        public GeneratorPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "GeneratorIndex";
        }

        public IWebElement EmailInput => _webDriver.FindElement(By.Id("Input_Email"));
        public IWebElement PasswordInput => _webDriver.FindElement(By.Id("Input_Password"));
        public IWebElement RememberMeCheck => _webDriver.FindElement(By.Id("Input_RememberMe"));
        public IWebElement SubmitButton => _webDriver.FindElement(By.Id("login-submit"));
        public IWebElement NavButton => _webDriver.FindElement(By.Id("generator-anchor"));
        public IWebElement Header => _webDriver.FindElement(By.Id("question-header"));
        public IWebElement Description => _webDriver.FindElement(By.Id("question-descr"));
        public IWebElement GeneratorButton => _webDriver.FindElement(By.Id("mood-anchor"));

        public void EnterEmail(string email)
        {
            EmailInput.Clear();
            EmailInput.SendKeys(email);
        }

        public void EnterPassword(string password)
        {
            PasswordInput.Clear();
            PasswordInput.SendKeys(password);
        }

        public void GoToGenerator()
        {
            GeneratorButton.Click();
        }

        public void GoToGeneratorIndex()
        {
            NavButton.Click();
        }

        public void Login()
        {
            SubmitButton.Click();
        }

        public bool DescriptionHasText(string text)
        {
            return Description.Text.Contains(text);
        }
        public bool HeaderHasText(string text)
        {
            return Header.Text.Contains(text);
        }

    }
}
