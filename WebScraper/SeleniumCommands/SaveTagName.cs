﻿using OpenQA.Selenium;

namespace WebScraper.SeleniumCommands;

public class SaveTagName : ICommand
{
    public string? Path { get; set; }
    public string? Name { get; set; }
        
    public ValueTask<string?> Execute(IWebDriver? driver)
    {
        return ValueTask.FromResult(driver!.FindElement(By.XPath(Path)).TagName)!;
    }
}