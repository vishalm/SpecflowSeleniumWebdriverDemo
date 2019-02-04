using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using EKE2EPages.Config;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace EKE2ESpecs.Features
{
    [Binding]
    public class GeneralHooks
    {
        // For additional details on SpecFlow hooks see http://go.specflow.org/doc-hooks
        private IWebDriver _driver;

        [BeforeScenario]
        public void BeforeScenario()
        {
            WebdriverConfig webdriverConfig = new WebdriverConfig();
            _driver = webdriverConfig.GetLocalChromeDriver();
            ScenarioContext.Current.Add("currentDriver", _driver);
        }

        [AfterScenario]
        public void AfterScenario()
        {
            _driver?.Quit();
        }
    }
}
