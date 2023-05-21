using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Refresh : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Navigate().Refresh();
        return ValueTask.FromResult("")!;
    }
}