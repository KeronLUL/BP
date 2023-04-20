using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

internal interface ICommand
{
    public IWebDriver? Driver { get; set; }
}