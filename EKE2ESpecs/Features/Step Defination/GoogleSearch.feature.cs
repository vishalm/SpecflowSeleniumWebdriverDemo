using System;
using TechTalk.SpecFlow;
using EKE2EPages.Pages;
using OpenQA.Selenium;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EKE2ESpecs.Features.Step_Defination
{
    [Binding]
    public class GoogleSearchSteps
    {
        private IWebDriver _driver;
        

        [Given(@"I am on google search")]
        public void GivenIAmOnGoogleSearch()
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            GoogleHomeSearch googleHomeSearch = new GoogleHomeSearch(_driver);
            googleHomeSearch.GotoHomePage();
        }

        [Given(@"I search for ""(.*)""")]
        public void GivenISearchFor(string searchString)
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            GoogleHomeSearch googleHomeSearch = new GoogleHomeSearch(_driver);
            googleHomeSearch.AddKeyWordToSearchBox(searchString);
        }

        [When(@"I search")]
        public void WhenISearch()
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            GoogleHomeSearch googleHomeSearch = new GoogleHomeSearch(_driver);
            googleHomeSearch.SubmitSearch();
        }

        [Then(@"I should see the solution of universal problem")]
        public void ThenIShouldSeeTheSolutionOfUniversalProblem()
        {
            _driver = ScenarioContext.Current.Get<IWebDriver>("currentDriver");
            GoogleHomeSearch googleHomeSearch = new GoogleHomeSearch(_driver);
            String searchResult = googleHomeSearch.SearchResult();
            Assert.IsTrue(searchResult.Contains("Universal"));

        }


    }
}
