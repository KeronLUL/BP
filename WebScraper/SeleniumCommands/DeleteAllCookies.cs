using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class DeleteAllCookies : ICommand
{
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Manage().Cookies.DeleteAllCookies();
        return ValueTask.FromResult("")!;
    }
}