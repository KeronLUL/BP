using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Back : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Navigate().Back();
        return ValueTask.FromResult<string?>(null);
    }
}