using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

internal interface ICommand
{
    ValueTask<string?> Execute(IWebDriver? driver);
}