using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace WebScraper.SeleniumCommands;

public class MoveToElement : ICommand<int>
{
    public string? Path { get; set; }
        
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        var element = wait.Until(ExpectedConditions.ElementIsVisible(By.XPath(Path)));
        var action = new Actions(driver);
        action.MoveToElement(element).Perform();
        return ValueTask.FromResult(0);
    }
}