using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Navigate : ICommand<int>
{
    public string? Path { get; set; }
        
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Navigate().GoToUrl(Path);
        return ValueTask.FromResult(0);
    }
}