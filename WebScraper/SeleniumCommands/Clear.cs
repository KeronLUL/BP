using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Clear : ICommand<int>
{
    public string? Path { get; set; }
    
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.FindElement(By.XPath(Path)).Clear();
        return ValueTask.FromResult(0);
    }
}