using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Maximize : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        driver!.Manage().Window.Maximize();
        return Task.FromResult(0);
    }
}