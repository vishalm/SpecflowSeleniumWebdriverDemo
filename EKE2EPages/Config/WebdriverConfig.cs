using System;
using System.IO;
using System.Net.Mime;
using System.Runtime.Remoting.Channels;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using System.Drawing;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Drawing.Imaging;
using OpenQA.Selenium.Interactions;




namespace EKE2EPages.Config
{
    public class WebdriverConfig
    {
        private static string _environment;
        private static string _girdEnv;
        private static string _remoteRun;
        private static string _remoteRunSauceLab;
        private static string _defaultBrowser;
        public static string _driverPath;
        private static int _defaultTimeOut = 120;
        public static IWebDriver Driver { get; set; }
        public IWebElement Driver2 = null;
        private static string _isSSORun;
        protected static string Environment
        {
            get { return _environment; }
        }

        protected static string GridEnv
        {
            get { return _girdEnv; }
        }
        static WebdriverConfig()
        {
            _environment = System.Configuration.ConfigurationManager.AppSettings["active_env"];
            _girdEnv = System.Configuration.ConfigurationManager.AppSettings["active_grid"];
            _remoteRun = System.Configuration.ConfigurationManager.AppSettings["remote_run"];
            _remoteRunSauceLab = System.Configuration.ConfigurationManager.AppSettings["remote_run_saucelab"];
            _defaultBrowser = System.Configuration.ConfigurationManager.AppSettings["default_browser"];
            _driverPath = System.Configuration.ConfigurationManager.AppSettings["chrome_ie_driver_path"];
            _isSSORun = System.Configuration.ConfigurationManager.AppSettings["isSSO"];
        }


        protected static string RemoteRun
        {
            get { return _remoteRun; }
        }

        public string GridContorllerUrl()
        {
            return System.Configuration.ConfigurationManager.AppSettings[GridEnv + "_url"];
        }


        public IWebDriver GetRemoteChromeDriver()
        {
            ChromeOptions options = GetChromeProfile();
            return new RemoteWebDriver(new Uri("<GridHubURL>"), options.ToCapabilities(), TimeSpan.FromSeconds(600));

        }

        public IWebDriver GetLocalChromeDriver()
        {
            
            ChromeDriver driver = new ChromeDriver(GetChromeProfile());
            driver.Manage().Window.Maximize();
            return driver;

        }

        private ChromeOptions GetChromeProfile()
        {
            ChromeOptions chromeOptions = new ChromeOptions();
            string downloadPath = _driverPath;
            //System.Environment.SetEnvironmentVariable("webdriver.chrome.driver", @"..\..\bin\Debug\chromedriver.exe");
            //System.setProperty("webdriver.chrome.driver", @"..\..\bin\Debug\chromedriver.exe");

            chromeOptions.AddUserProfilePreference("download.default_directory", downloadPath);
            //chromeOptions.AddUserProfilePreference("intl.accept_languages", "nl");
            chromeOptions.AddUserProfilePreference("disable-popup-blocking", true);
            chromeOptions.AddArgument("test-type");
            chromeOptions.AddArgument("disable-extensions");
            chromeOptions.AddExcludedArgument("ignore-certifcate-errors");
            chromeOptions.AddUserProfilePreference("disable-extensions",true);
            chromeOptions.AddAdditionalCapability("useAutomationExtension", false);

            //chromeOptions.BinaryLocation = "C:\\Users\\S799985\\source\\repos\\EKE2ESpecs\\chromedriver.exe";
            return chromeOptions;
        }

        public void CloseCurrentWindow()
        {
            Driver.Close();
        }

        public void QuitDriver()
        {
            Driver.Quit();
            Driver = null;
        }

        public bool IsDriverOpen()
        {
            return Driver != null;
        }

        /// <summary>
        /// Clears all the cookies stored in the current session.
        /// </summary>
        public void ClearAllCookies()
        {
            Driver.Manage().Cookies.DeleteAllCookies();
        }

        public void TakeScreenshot(string featureContext, string scenarioContext)
        {
            try
            {
                scenarioContext = TruncateString(scenarioContext);
                string fileNameBase = string.Format("{0}_{1}",
                                                    scenarioContext,
                                                    DateTime.Now.ToString("yyMMdd_HHmmss"));
                fileNameBase = fileNameBase.Replace(' ', '_');
                //var resultFolder = Path.Combine(Directory.GetCurrentDirectory(), "AutomationTestResult");
                string resultFolder = Path.Combine(_driverPath, "AutomationTestResult");
                if (!Directory.Exists(resultFolder))
                { Directory.CreateDirectory(resultFolder); }

                var baseDir = Path.Combine(resultFolder, featureContext, scenarioContext);
                if (!Directory.Exists(baseDir))
                    Directory.CreateDirectory(baseDir);

                ITakesScreenshot takesScreenshot = Driver as ITakesScreenshot;

                if (takesScreenshot != null)
                {
                    var screenshot = takesScreenshot.GetScreenshot();
                    string screenshotFilePath = Path.Combine(baseDir, fileNameBase + ".png");
                    //screenshot.SaveAsFile(screenshotFilePath, ImageFormat.Png);
                    Console.WriteLine("Screenshot: {0}", new Uri(screenshotFilePath));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Unable to take screenshot" + ex);
            }
        }

        private string TruncateString(string value)
        {
            int maxLength = 20;
            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }


        public void OpenANewTab()
        {
            Driver2 = Driver.FindElement(By.CssSelector("body"));
            Driver2.SendKeys(Keys.Control + "t");
        }

        public void SwitchBackToBaseTab()
        {
            OpenQA.Selenium.Interactions.Actions action = new Actions(Driver);
            action.SendKeys(Keys.Control + "\t").Build().Perform();
            Driver.SwitchTo().DefaultContent();
        }

        public void SwitchBackToPreviousTab()
        {
            Actions action = new Actions(Driver);
            action.SendKeys(Keys.Control + "\t").Build().Perform();
            action.SendKeys(Keys.Control + "\t").Build().Perform();
            Driver.SwitchTo().DefaultContent();
        }

        public void SwitchToFirst()
        {
            Actions action = new Actions(Driver);
            action.SendKeys(Keys.Control + "\t").Build().Perform();
            Driver.SwitchTo().Window(Driver.WindowHandles.First());
        }
    }
}
