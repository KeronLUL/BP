using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveAttribute : ICommand
{
    public string? Path { get; set; }
    public string? Attribute { get; set; }
    public string? Name { get; set; }
    
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        return ValueTask.FromResult(driver!.FindElement(By.XPath(Path)).GetAttribute(Attribute))!;
    }
}