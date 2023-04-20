using WebScraper.SeleniumCommands;

namespace WebScraper.Json.Entities;

public class Command
{
    public Click? Click { get; set; }
    public SaveText? SaveText { get; set; }
    public SaveAttribute? SaveAttribute { get; set; }
    public Clear? Clear { get; set; }
    public SendKeys? SendKeys { get; set; }
    public ImplicitWait? ImplicitWait { get; set; }
    public Submit? Submit { get; set; }
    public Navigate? Navigate { get; set; }
    public WaitUntilExists? WaitUntilExists { get; set; }
    public WaitUntilClickable? WaitUntilClickable { get; set; }
    public MoveToElement? MoveToElement { get; set; }
}