using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendKeysAlert : ICommand
{
    public string? Text { get; set; }
        
    public Task Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.SendKeys(Text);
        alert.Accept();
        return Task.FromResult(0);
    }
}