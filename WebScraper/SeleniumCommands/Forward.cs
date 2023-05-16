using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Forward : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        driver!.Navigate().Forward();
        return Task.FromResult(0);
    }
}