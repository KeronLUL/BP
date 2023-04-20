using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;

namespace WebScraper.SeleniumDriver
{
    public class SeleniumWebDriver
    {
        public IWebDriver? Driver { get; private set; }

        public void SetUp(string? url, string driver)
        {
            switch(driver)
            {
                case "Chrome":
                    var optionsChrome = new ChromeOptions();
                    optionsChrome.AddArguments("--headless=new", "--window-size=1920,1080");
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
            
            Driver!.Url = url;
        }

        public void Quit()
        {
            Driver?.Quit();
        }

        public void Close()
        {
            Driver?.Close();
        }
    }
}