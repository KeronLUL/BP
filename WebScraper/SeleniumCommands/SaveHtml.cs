using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveHtml : ICommand
{
    public string? Name { get; set; }
    public Task Execute(IWebDriver? driver)
    {
        return Task.FromResult(driver!.PageSource);
    }
}