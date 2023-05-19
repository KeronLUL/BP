using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Refresh : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Navigate().Refresh();
        return ValueTask.FromResult(0);
    }
}