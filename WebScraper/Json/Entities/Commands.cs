namespace WebScraper.Json.Entities;

public class Command
{
    public Click? Click { get; set; }
    public Text? Text { get; set; }
}

public class Click
{
    public List<Arg>? args { get; set; }
}

public class Text
{
    public List<Arg>? args { get; set; }
}

public class Arg
{
    public string? path { get; set; }
}