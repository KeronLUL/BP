using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Forward : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Navigate().Forward();
        return ValueTask.FromResult(0);
    }
}