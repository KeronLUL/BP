using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Navigate : ICommand
{
    public string? Path { get; set; }
        
    public Task Execute(IWebDriver? driver)
    {
        driver!.Navigate().GoToUrl(Path);
        return Task.FromResult(0);
    }
}