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
        public string? Path { get; set; }

        public Task Execute(IWebDriver? driver)
        { 
            driver!.FindElement(By.XPath(Path)).Click();
            return Task.FromResult("");
        }
    }
    public class SaveText : ICommand
    {
        public string? Path { get; set; }
        public string? Name { get; set; }
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(driver!.FindElement(By.XPath(Path)).Text);
        }
    }

    public class SaveAttribute : ICommand
    {
        public string? Path { get; set; }
        public string? Attribute { get; set; }
        public string? Name { get; set; }
    
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(driver!.FindElement(By.XPath(Path)).GetAttribute(Attribute));
        }
    }
    
    public class Clear : ICommand
    {
        public string? Path { get; set; }
    
        public Task Execute(IWebDriver? driver)
        {
            driver!.FindElement(By.XPath(Path)).Clear();
            return Task.FromResult(0);
        }
    }
    
    public class Submit : ICommand
    {
        public string? Path { get; set; }
    
        public Task Execute(IWebDriver? driver)
        {
            driver!.FindElement(By.XPath(Path)).Submit();
            return Task.FromResult(0);
        }
    }
    
    public class SendKeys : ICommand
    {
        public string? Path { get; set; }
        public string? Text { get; set; }
        public Task Execute(IWebDriver? driver)
        {
            driver!.FindElement(By.XPath(Path)).SendKeys(Text);
            return Task.FromResult(0);
        }
    }
    
    public class MoveToElement : ICommand
    {
        public string? Path { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Path)));
            var action = new Actions(driver);
            action.MoveToElement(element).Perform();
            return Task.FromResult(0);
        }
    }
    
    public class Navigate : ICommand
    {
        public string? Path { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            driver!.Navigate().GoToUrl(Path);
            return Task.FromResult(0);
        }
    }
    
    public class ImplicitWait : ICommand
    {
        public int Time { get; set; }
        public Task Execute(IWebDriver? driver)
        {
            driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
            return Task.FromResult(0);
        }
    }
    
    public class WaitUntilExists : ICommand
    {
        public string? Path { get; set; }
        public int Time { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Time));
            wait.Until(ExpectedConditions.ElementExists(By.XPath(Path)));
            return Task.FromResult(0);
        }
    }
    
    public class WaitUntilClickable : ICommand
    {
        public string? Path { get; set; }
        public int Time { get; set; }
    
        public Task Execute(IWebDriver? driver)
        {
            var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Time));
            wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Path)));
            return Task.FromResult(0);
        }
    }
    
    public class SaveHtml : ICommand
    {
        public string? Name { get; set; }
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(driver!.PageSource);
        }
    }
    
    public class SaveTitle : ICommand
    {
        public string? Name { get; set; }
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(driver!.Title);
        }
    }
    
    public class ExecuteJavaScript : ICommand
    {
        public string? Script { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(((IJavaScriptExecutor)driver!).ExecuteScript(Script));
        }
    }
    
    public class SaveCssValue : ICommand
    {
        public string? Path { get; set; }
        public string? Property { get; set; }
        public string? Name { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(driver!.FindElement(By.XPath(Path)).GetCssValue(Property));
        }
    }
    
    public class SaveTagName : ICommand
    {
        public string? Path { get; set; }
        public string? Name { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            return Task.FromResult(driver!.FindElement(By.XPath(Path)).TagName);
        }
    }
    
    public class Maximize : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            driver!.Manage().Window.Maximize();
            return Task.FromResult(0);
        }
    }
    
    public class Refresh : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            driver!.Navigate().Refresh();
            return Task.FromResult(0);
        }
    }
    
    public class Back : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            driver!.Navigate().Back();
            return Task.FromResult(0);
        }
    }
    
    public class Forward : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            driver!.Navigate().Forward();
            return Task.FromResult(0);
        }
    }
    
    public class SendReturn : ICommand
    {
        public string? Path { get; set; }
        public Task Execute(IWebDriver? driver)
        {
            driver!.FindElement(By.XPath(Path)).SendKeys(Keys.Return);
            return Task.FromResult(0);
        }
    }
    
    public class DeleteAllCookies : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            driver!.Manage().Cookies.DeleteAllCookies();
            return Task.FromResult(0);
        }
    }
    
    public class AcceptAlert : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            var alert = driver!.SwitchTo().Alert();
            alert.Accept();
            return Task.FromResult(0);
        }
    }
    
    public class DismissAlert : ICommand
    {
        public Task Execute(IWebDriver? driver)
        {
            var alert = driver!.SwitchTo().Alert();
            alert.Dismiss();
            return Task.FromResult(0);
        }
    }
    
    public class SendKeysAlert : ICommand
    {
        public string? Text { get; set; }
        
        public Task Execute(IWebDriver? driver)
        {
            var alert = driver!.SwitchTo().Alert();
            alert.SendKeys(Text);
            alert.Accept();
            return Task.FromResult(0);
        }
    }
}