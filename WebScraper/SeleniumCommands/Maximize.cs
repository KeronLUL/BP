using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Maximize : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Manage().Window.Maximize();
        return ValueTask.FromResult("")!;
    }
}