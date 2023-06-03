using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class AcceptAlert : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.Accept();
        return ValueTask.FromResult<string?>(null);
    }
}