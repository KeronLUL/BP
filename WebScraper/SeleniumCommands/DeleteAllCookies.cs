using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class DeleteAllCookies : ICommand<int>
{
    public ValueTask<int> Execute(IWebDriver? driver)
    {
        driver!.Manage().Cookies.DeleteAllCookies();
        return ValueTask.FromResult(0);
    }
}