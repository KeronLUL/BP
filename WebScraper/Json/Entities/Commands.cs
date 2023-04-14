namespace WebScraper.Json.Entities;

public class CommandFactory
{
    public List<Arguments>? args { get; set; }
}

public class Command
{
    public Click? Click { get; set; }
    public SaveText? SaveText { get; set; }
    public SaveAttribute? SaveAttribute { get; set; }
    public Clear? Clear { get; set; }
    public SendKeys? SendKeys { get; set; }
    public ImplicitWait? ImplicitWait { get; set; }
    public Submit? Submit { get; set; }
}

public class SaveAttribute : CommandFactory
{
}

public class Click : CommandFactory
{
}

public class Clear : CommandFactory
{
}

public class SendKeys : CommandFactory
{
}

public class ImplicitWait : CommandFactory
{
}

public class Submit : CommandFactory
{
}

public class SaveText : CommandFactory
{
}

public class Arguments
{
    public string? path { get; set; }
    public string? text { get; set; }
    public string? name { get; set; }
    public string? attribute { get; set; }
    public int? time { get; set; }
}