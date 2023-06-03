using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendKeys : ICommand
{
    public string? Path { get; set; }
    public string? Text { get; set; }
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).SendKeys(Text);
        return ValueTask.FromResult<string?>(null);
    }
}