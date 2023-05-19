using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SendKeys : ICommand<int>
{
    public string? Path { get; set; }
    public string? Text { get; set; }
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).SendKeys(Text);
        return ValueTask.FromResult(0);
    }
}