using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendKeysAlert : ICommand
{
    public string? Text { get; set; }
        
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.SendKeys(Text);
        alert.Accept();
        return ValueTask.FromResult("")!;
    }
}