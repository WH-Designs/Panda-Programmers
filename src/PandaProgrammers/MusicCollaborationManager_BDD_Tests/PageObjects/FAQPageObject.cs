using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicCollaborationManager_BDD_Tests.PageObjects
{
    public class FAQPageObject : PageObject
    {
        public FAQPageObject(IWebDriver webDriver) : base(webDriver)
        {
            _pageName = "FAQ";
        }

        public IWebElement FAQBtn => _webDriver.FindElement(By.Id("FAQ_btn"));
        public IWebElement GeneralBtn => _webDriver.FindElement(By.Id("general_btn"));
        public IWebElement GeneralContent => _webDriver.FindElement(By.Id("general_content"));
        public IWebElement QuestBtn => _webDriver.FindElement(By.Id("quest_btn"));
        public IWebElement QuestContent => _webDriver.FindElement(By.Id("quest_content"));
        public IWebElement QuestVid => _webDriver.FindElement(By.Id("quest_vid"));
        public IWebElement MoodBtn => _webDriver.FindElement(By.Id("mood_btn"));
        public IWebElement MoodContent => _webDriver.FindElement(By.Id("mood_content"));
        public IWebElement MoodVid => _webDriver.FindElement(By.Id("mood_vid"));
        public IWebElement RelateBtn => _webDriver.FindElement(By.Id("relate_btn"));
        public IWebElement RelateContent => _webDriver.FindElement(By.Id("relate_content"));
        public IWebElement RelateVid => _webDriver.FindElement(By.Id("relate_vid"));
        public IWebElement TimeBtn => _webDriver.FindElement(By.Id("time_btn"));
        public IWebElement TimeContent => _webDriver.FindElement(By.Id("time_content"));
        public IWebElement TimeVid => _webDriver.FindElement(By.Id("time_vid"));
        public IWebElement TopABtn => _webDriver.FindElement(By.Id("topa_btn"));
        public IWebElement TopAContent => _webDriver.FindElement(By.Id("topa_content"));
        public IWebElement TopAVid => _webDriver.FindElement(By.Id("topa_vid"));
        public IWebElement TopTBtn => _webDriver.FindElement(By.Id("topt_btn"));
        public IWebElement TopTContent => _webDriver.FindElement(By.Id("topt_content"));
        public IWebElement TopTVid => _webDriver.FindElement(By.Id("topt_vid"));
        public IWebElement InputBtn => _webDriver.FindElement(By.Id("input_btn"));
        public IWebElement InputContent => _webDriver.FindElement(By.Id("input_content"));
        public IWebElement InputVid => _webDriver.FindElement(By.Id("input_vid"));
        public IWebElement AIBtn => _webDriver.FindElement(By.Id("ai_btn"));
        public IWebElement AIContent => _webDriver.FindElement(By.Id("ai_content"));
        public IWebElement AIVid => _webDriver.FindElement(By.Id("ai_vid"));

        public void GoToFAQ()
        {
            FAQBtn.Click();
        }

        public void OpenAll()
        {
            GeneralBtn.Click();
            QuestBtn.Click();
            MoodBtn.Click();
            RelateBtn.Click();
            TimeBtn.Click();
            TopABtn.Click();
            TopTBtn.Click();
            AIBtn.Click();
            InputBtn.Click();
        }

    }
}
