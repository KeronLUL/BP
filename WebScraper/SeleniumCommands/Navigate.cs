using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Navigate : ICommand
{
    public string? Path { get; set; }
        
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Navigate().GoToUrl(Path);
        return ValueTask.FromResult("")!;
    }
}