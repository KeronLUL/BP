using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendReturn : ICommand<int>
{
    public string? Path { get; set; }
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).SendKeys(Keys.Return);
        return ValueTask.FromResult(0);
    }
}