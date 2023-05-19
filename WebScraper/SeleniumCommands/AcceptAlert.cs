using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class AcceptAlert : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.Accept();
        return ValueTask.FromResult(0);
    }
}