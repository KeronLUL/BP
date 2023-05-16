using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class DeleteAllCookies : ICommand
{
    public Task Execute(IWebDriver? driver)
    {
        driver!.Manage().Cookies.DeleteAllCookies();
        return Task.FromResult(0);
    }
}