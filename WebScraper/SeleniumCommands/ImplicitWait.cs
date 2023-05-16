using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class ImplicitWait : ICommand
{
    public int Time { get; set; }
    public Task Execute(IWebDriver? driver)
    {
        driver!.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(Time);
        return Task.FromResult(0);
    }
}