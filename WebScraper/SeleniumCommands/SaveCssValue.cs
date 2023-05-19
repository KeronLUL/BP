using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveCssValue : ICommand<string>
{
    public string? Path { get; set; }
    public string? Property { get; set; }
    public string? Name { get; set; }
        
    public ValueTask<string> Execute(IWebDriver? driver)
    {
        return ValueTask.FromResult(driver!.FindElement(By.XPath(Path)).GetCssValue(Property));
    }
}