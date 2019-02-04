using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EKE2EPages.Config;
using OpenQA.Selenium.Support.PageObjects;
using OpenQA.Selenium;


namespace EKE2EPages.Pages
{
    public class GoogleHomeSearch : WebdriverConfig

    {
        private IWebDriver driver;

        [FindsBy(How = How.CssSelector, Using = "input.gLFyf")]
        private IWebElement SearchTextBox;
        
        [FindsBy(How = How.CssSelector, Using = "div.FPdoLc input")]
        public IWebElement GoogleSearchButton;

        [FindsBy(How = How.Id, Using = "gb")]
        public IWebElement GoogleHeader;

        [FindsBy(How = How.CssSelector, Using = "div.srg h3")]
        public IWebElement SearchResultText;

        public GoogleHomeSearch(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        public void GotoHomePage()
        {
            driver.Navigate().GoToUrl("http://www.google.com");

        }

        public void AddKeyWordToSearchBox(string searchString) {
            if (searchString == "")
            {
                searchString = FakeSearchString();
            }
            SearchTextBox.SendKeys(searchString);
        }

        public void SubmitSearch() {
            GoogleHeader.Click();
            GoogleSearchButton.Click();

        }

        public String SearchResult() {

            return SearchResultText.Text;
        }
        private String FakeSearchString() {
            return Faker.Company.Name();
        }
    }
}
