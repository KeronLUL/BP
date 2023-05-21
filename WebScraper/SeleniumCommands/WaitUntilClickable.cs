using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraper.SeleniumCommands;

public class WaitUntilClickable : ICommand
{
    public string? Path { get; set; }
    public int Time { get; set; }
    
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(Time));
        wait.Until(ExpectedConditions.ElementToBeClickable(By.XPath(Path)));
        return ValueTask.FromResult("")!;
    }
}