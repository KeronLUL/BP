using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Submit : ICommand
{
    public string? Path { get; set; }
    
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).Submit();
        return ValueTask.FromResult<string?>(null);
    }
}