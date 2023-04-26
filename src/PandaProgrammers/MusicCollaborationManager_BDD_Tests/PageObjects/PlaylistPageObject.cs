using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class PlaylistPageObject : PageObject
    {
        public PlaylistPageObject(IWebDriver webDriver) : base(webDriver)
        {
            // using a named page (in Common.cs)
            _pageName = "Playlist";
        }

        public IWebElement CommentSubmitButton => _webDriver.FindElement(By.Id("comment-submit-button"));
        public IWebElement CommentForm => _webDriver.FindElement(By.Id("comment-form"));
        public IWebElement CommentInput => _webDriver.FindElement(By.Id("comment-message-input"));

        public void SubmitComment()
        {
            CommentSubmitButton.Click();
        }

        public void EnterComment(string text)
        {
            CommentInput.Clear();
            CommentInput.SendKeys(text);
        }
    }
}
