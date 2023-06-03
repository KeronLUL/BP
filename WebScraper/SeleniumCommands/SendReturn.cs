using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendReturn : ICommand
{
    public string? Path { get; set; }
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).SendKeys(Keys.Return);
        return ValueTask.FromResult<string?>(null);
    }
}