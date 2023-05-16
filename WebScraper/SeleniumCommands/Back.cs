using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Back : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        driver!.Navigate().Back();
        return Task.FromResult(0);
    }
}