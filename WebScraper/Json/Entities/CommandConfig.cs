namespace WebScraper.Json.Entities;

public class CommandConfig
{
    public string? Name { get; set; }
    public IDictionary<string, object>? Args { get; set; }
}