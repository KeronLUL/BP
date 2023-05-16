using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Refresh : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        driver!.Navigate().Refresh();
        return Task.FromResult(0);
    }
}