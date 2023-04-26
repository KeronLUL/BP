using WebScraper.SeleniumCommands;

namespace WebScraper.Json.Entities;

public class Command
{
    public AcceptAlert? AcceptAlert { get; set; }
    public Back? Back { get; set; }
    public Clear? Clear { get; set; }
    public Click? Click { get; set; }
    public DeleteAllCookies? DeleteAllCookies { get; set; }
    public DismissAlert? DismissAlert { get; set; }
    public ExecuteJavaScript? ExecuteJavaScript { get; set; }
    public Forward? Forward { get; set; }
    public ImplicitWait? ImplicitWait { get; set; }
    public Maximize? Maximize { get; set; }
    public MoveToElement? MoveToElement { get; set; }
    public Navigate? Navigate { get; set; }
    public Refresh? Refresh { get; set; }
    public SaveAttribute? SaveAttribute { get; set; }
    public SaveCssValue? SaveCssValue { get; set; }
    public SaveHtml? SaveHtml { get; set; }
    public SaveTagName? SaveTagName { get; set; }
    public SaveText? SaveText { get; set; }
    public SaveTitle? SaveTitle { get; set; }
    public SendKeys? SendKeys { get; set; }
    public SendKeysAlert? SendKeysAlert { get; set; }
    public SendReturn? SendReturn { get; set; }
    public Submit? Submit { get; set; }
    public WaitUntilClickable? WaitUntilClickable { get; set; }
    public WaitUntilExists? WaitUntilExists { get; set; }
}