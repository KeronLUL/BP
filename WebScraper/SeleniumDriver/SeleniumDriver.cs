using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Safari;
using WebScraper.Arguments;

namespace WebScraper.SeleniumDriver
{
    public class SeleniumWebDriver
    {
        public IWebDriver? Driver { get; private set; }

        public void SetUp(string? url, string driver)
        {
            switch (driver)
            {
                case "Chrome":
                    var optionsChrome = new ChromeOptions();
                    if (Argument.Headless()) optionsChrome.AddArguments("--headless=new");
                    if (Argument.Maximized()) optionsChrome.AddArguments("start-maximized");
                    Driver = new ChromeDriver(optionsChrome);
                    break;
                case "Firefox":
                    var optionsFirefox = new FirefoxOptions();
                    if (Argument.Headless()) optionsFirefox.AddArguments("--headless");
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