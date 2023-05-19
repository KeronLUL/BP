using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Click : ICommand<int>
{
    public string? Path { get; set; }

    public ValueTask<int> Execute(IWebDriver? driver)
    { 
        driver!.FindElement(By.XPath(Path)).Click();
        return ValueTask.FromResult(0);
    }
}