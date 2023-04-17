
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Safari;

namespace SeleniumGenerated
{
    public static class SeleniumWebDriver
    {
        public static IWebDriver Driver { get; set; }

        public static void SetUp(string Url, string driver) 
        {
            switch(driver)
            {
                case "Chrome":
                    var optionsChrome = new ChromeOptions();
                    optionsChrome.AddArguments("--headless=new", "--window-size=1920,1080", "--disable-gpu", "--no-sandbox");
                    Driver = new ChromeDriver(optionsChrome);
                    break;
                case "Firefox":
                    var optionsFirefox = new FirefoxOptions();
                    optionsFirefox.AddArguments("--headless=new", "--window-size=1920,1080");
                    Driver = new FirefoxDriver(optionsFirefox);
                    break;
                case "Safari":
                    Driver = new SafariDriver();
                    break;
            }
            
            Driver.Url = Url;
        }

        public static void Navigate(string url)
        {
            Driver.Navigate().GoToUrl(url);
        }

        public static void Quit()
        {
            Driver.Quit();
        }

        public static void Close()
        {
            Driver.Close();
        }
    }
}