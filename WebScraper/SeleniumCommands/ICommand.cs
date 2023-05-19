using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

internal interface ICommand<T>
{
    ValueTask<T> Execute(IWebDriver? driver);
}