using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Navigate : ICommand
{
    public string? Url { get; set; }
        
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Navigate().GoToUrl(Url);
        return ValueTask.FromResult<string?>(null);
    }
}