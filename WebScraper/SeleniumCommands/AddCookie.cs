using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class AddCookie : ICommand
{
    public string? Key { get; set; }
    public string? Value { get; set; }
    
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        driver!.Manage().Cookies.AddCookie(new Cookie(Key, Value));
        return ValueTask.FromResult<string?>(null);
    }
}