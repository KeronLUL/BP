using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Maximize : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Manage().Window.Maximize();
        return ValueTask.FromResult(0);
    }
}