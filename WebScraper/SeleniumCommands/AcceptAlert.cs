using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class AcceptAlert : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.Accept();
        return Task.FromResult(0);
    }
}