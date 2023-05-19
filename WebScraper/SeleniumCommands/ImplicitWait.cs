using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class ImplicitWait : ICommand<int>
{
    public int Time { get; set; }
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
        return ValueTask.FromResult(0);
    }
}