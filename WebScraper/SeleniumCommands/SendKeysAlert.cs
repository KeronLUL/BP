using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendKeysAlert : ICommand<int>
{
    public string? Text { get; set; }
        
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        var alert = driver!.SwitchTo().Alert();
        alert.SendKeys(Text);
        alert.Accept();
        return ValueTask.FromResult(0);
    }
}