using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveHtml : ICommand
{
    public string? Name { get; set; }
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        return ValueTask.FromResult(driver!.PageSource)!;
    }
}