using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveCssValue : ICommand
{
    public string? Path { get; set; }
    public string? Property { get; set; }
    public string? Name { get; set; }
        
    public Task Execute(IWebDriver? driver)
    {
        return Task.FromResult(driver!.FindElement(By.XPath(Path)).GetCssValue(Property));
    }
}