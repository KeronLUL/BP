using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Back : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Navigate().Back();
        return ValueTask.FromResult(0);
    }
}