using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Clear : ICommand
{
    public string? Path { get; set; }
    
    public Task Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).Clear();
        return Task.FromResult(0);
    }
}