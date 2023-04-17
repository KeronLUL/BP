﻿
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
                    optionsChrome.AddArgument("--headless=new");
                    Driver = new ChromeDriver(optionsChrome);
                    break;
                case "Firefox":
                    var optionsFirefox = new FirefoxOptions();
                    optionsFirefox.AddArgument("--headless=new");
                    Driver = new FirefoxDriver(optionsFirefox);
                    break;
                case "Safari":
                    Driver = new SafariDriver();
                    break;
                default:
                    var options = new FirefoxOptions();
                    options.AddArgument("--headless=new");
                    Driver = new FirefoxDriver(options);
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