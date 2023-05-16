using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendReturn : ICommand
{
    public string? Path { get; set; }
    public Task Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).SendKeys(Keys.Return);
        return Task.FromResult(0);
    }
}