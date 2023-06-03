using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class ImplicitWait : ICommand
{
    public int Time { get; set; }
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
        return ValueTask.FromResult<string?>(null);
    }
}