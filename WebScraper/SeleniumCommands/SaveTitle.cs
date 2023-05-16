using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveTitle : ICommand
{
    public string? Name { get; set; }
    public Task Execute(IWebDriver? driver)
    {
        return Task.FromResult(driver!.Title);
    }
}