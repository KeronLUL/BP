using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class DismissAlert : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.Dismiss();
        return ValueTask.FromResult(0);
    }
}