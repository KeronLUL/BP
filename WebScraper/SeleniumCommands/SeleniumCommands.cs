using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraper.SeleniumCommands
{
    public class Click : ICommand
    {
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
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        [JsonProperty("name")]
        public string? Name;

        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Path)).Text;
        }
    }
    
    public class SaveAttribute : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        [JsonProperty("attribute")]
        public string? Attribute;
        [JsonProperty("name")]
        public string? Name;
        
        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Path)).GetAttribute(Attribute);
        }
    }
    
    public class Clear : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        
        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).Clear();
        }
    }
    
    public class Submit : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        
        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).Submit();
        }
    }
    
    public class SendKeys : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        [JsonProperty("text")]
        public string? Text;
        
        public void Execute()
        {
            Driver!.FindElement(By.XPath(Path)).SendKeys(Text);
        }
    }
    
    public class MoveToElement : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
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
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        
        public void Execute()
        {
            Driver!.Navigate().GoToUrl(Path);
        }
    }
    
    public class ImplicitWait : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("time")]
        public int Time;
        
        public void Execute(int time)
        {
            Driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
        }
    }
    
    public class WaitUntilExists : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        [JsonProperty("time")]
        public int Time;
        
        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Time));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(Path)));
        }
    }
    
    public class WaitUntilClickable : ICommand
    {
        public IWebDriver? Driver { get; set; }
        [JsonProperty("path")]
        public string? Path;
        [JsonProperty("time")]
        public int Time;
        
        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Time));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Path)));
        }
    }
}