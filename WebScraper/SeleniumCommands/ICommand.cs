using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

internal interface ICommand
{
    Task Execute(IWebDriver? driver);
}