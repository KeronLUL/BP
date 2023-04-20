using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraper.SeleniumCommands
{
    public class Click : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).Click();
        }
    }

    public class SaveText : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;
        [JsonProperty("name"), Required]
        public string? Name;

        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Path)).Text;
        }
    }

    public class SaveAttribute : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;
        [JsonProperty("attribute"), Required]
        public string? Attribute;
        [JsonProperty("name"), Required]
        public string? Name;

        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Path)).GetAttribute(Attribute);
        }
    }

    public class Clear : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).Clear();
        }
    }

    public class Submit : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).Submit();
        }
    }

    public class SendKeys : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;
        [JsonProperty("text"), Required]
        public string? Text;

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).SendKeys(Text);
        }
    }

    public class MoveToElement : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;

        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Path)));
            var action = new Actions(Driver);
            action.MoveToElement(element).Perform();
        }
    }

    public class Navigate : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;

        public void Execute()
        {
            Driver!.Navigate().GoToUrl(Path);
        }
    }

    public class ImplicitWait : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("time"), Required]
        public int Time;

        public void Execute(int time)
        {
            Driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
        }
    }

    public class WaitUntilExists : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;
        [JsonProperty("time"), Required]
        public int Time;

        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Time));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(Path)));
        }
    }

    public class WaitUntilClickable : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path"), Required]
        public string? Path;
        [JsonProperty("time"), Required]
        public int Time;

        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Time));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Path)));
        }
    }
}