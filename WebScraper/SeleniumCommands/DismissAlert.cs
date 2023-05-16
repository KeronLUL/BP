using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class DismissAlert : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.Dismiss();
        return Task.FromResult(0);
    }
}