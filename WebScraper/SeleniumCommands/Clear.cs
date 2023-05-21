using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Clear : ICommand
{
    public string? Path { get; set; }
    
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).Clear();
        return ValueTask.FromResult("")!;
    }
}