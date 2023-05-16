using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Submit : ICommand
{
    public string? Path { get; set; }
    
    public Task Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).Submit();
        return Task.FromResult(0);
    }
}