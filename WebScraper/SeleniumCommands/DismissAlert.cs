using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class DismissAlert : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.Dismiss();
        return ValueTask.FromResult("")!;
    }
}