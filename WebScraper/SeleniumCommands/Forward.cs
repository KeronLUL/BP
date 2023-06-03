using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Forward : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Navigate().Forward();
        return ValueTask.FromResult<string?>(null);
    }
}