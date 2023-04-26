using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using WebScraper.Json.Entities;

namespace WebScraper.SeleniumCommands
{
    public class Click : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Args!.Path)).Click();
        }
    }
    public class SaveText : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Args!.Path)).Text;
        }
    }

    public class SaveAttribute : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Args!.Path)).GetAttribute(Args!.Attribute);
        }
    }

    public class Clear : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Args!.Path)).Clear();
        }
    }

    public class Submit : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Args!.Path)).Submit();
        }
    }

    public class SendKeys : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.FindElement(By.XPath(Args!.Path)).SendKeys(Args!.Text);
        }
    }

    public class MoveToElement : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Args!.Path)));
            var action = new Actions(Driver);
            action.MoveToElement(element).Perform();
        }
    }

    public class Navigate : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.Navigate().GoToUrl(Args!.Path);
        }
    }

    public class ImplicitWait : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute(int time)
        {
            Driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Args!.Time);
        }
    }

    public class WaitUntilExists : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Args!.Time));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(Args!.Path)));
        }
    }

    public class WaitUntilClickable : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            var wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(Args!.Time));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Args!.Path)));
        }
    }

    public class SaveHtml : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public string Execute()
        {
            return Driver!.PageSource;
        }
    }

    public class SaveTitle : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public string Execute()
        {
            return Driver!.Title;
        }
    }

    public class ExecuteJavaScript : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        
        public string Execute()
        {
            return (string)((IJavaScriptExecutor)Driver!).ExecuteScript("return document.title");
        }
    }
    
    public class ExecuteAsyncJavaScript : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public string Execute()
        {
            return (string)((IJavaScriptExecutor)Driver!).ExecuteAsyncScript("return document.title");
        }
    }

    public class SaveCssValue : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Args!.Path)).GetCssValue(Args!.Property);
        }
    }

    public class SaveTagName : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public string Execute()
        {
            return Driver!.FindElement(By.XPath(Args!.Path)).TagName;
        }
    }

    public class Maximize : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.Manage().Window.Maximize();
        }
    }

    public class Refresh : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.Navigate().Refresh();
        }
    }
    
    public class Back : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.Navigate().Back();
        }
    }
    
    public class Forward : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }

        public void Execute()
        {
            Driver!.Navigate().Forward();
        }
    }

    public class SendReturn : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        public void Execute()
        {
            Driver!.FindElement(By.XPath(Args!.Path)).SendKeys(Keys.Return);
        }
    }

    public class DeleteAllCookies : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        
        public void Execute()
        {
            Driver!.Manage().Cookies.DeleteAllCookies();
        }
    }

    public class AcceptAlert : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        
        public void Execute()
        {
            var alert = Driver!.SwitchTo().Alert();
            alert.Accept();
        }
    }
    
    public class DismissAlert : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        
        public void Execute()
        {
            var alert = Driver!.SwitchTo().Alert();
            alert.Dismiss();
        }
    }
    
    public class SendKeysAlert : ICommand
    {
        [JsonIgnore]
        public IWebDriver? Driver { get; set; }
        public Args? Args { get; set; }
        
        public void Execute()
        {
            var alert = Driver!.SwitchTo().Alert();
            alert.SendKeys(Args!.Text);
            alert.Accept();
        }
    }
}