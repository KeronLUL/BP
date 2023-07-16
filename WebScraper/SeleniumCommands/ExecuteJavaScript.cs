using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class ExecuteJavaScript : ICommand
{
    public string? Name { get; set; }
    public string? Script { get; set; }
        
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        return ValueTask.FromResult(((IJavaScriptExecutor)driver!).ExecuteScript(Script).ToString());
    }
}