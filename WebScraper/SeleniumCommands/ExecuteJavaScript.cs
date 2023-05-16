using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class ExecuteJavaScript : ICommand
{
    public string? Script { get; set; }
        
    public Task Execute(IWebDriver? driver)
    {
        return Task.FromResult(((IJavaScriptExecutor)driver!).ExecuteScript(Script));
    }
}