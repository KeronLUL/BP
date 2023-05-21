using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveTitle : ICommand
{
    public string? Name { get; set; }
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        return ValueTask.FromResult(driver!.Title)!;
    }
}