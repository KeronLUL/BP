﻿using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class Click : ICommand
{
    public string? Path { get; set; }

    public ValueTask<string?> Execute(IWebDriver? driver)
    { 
        driver!.FindElement(By.XPath(Path)).Click();
        return ValueTask.FromResult<string?>(null);
    }
}