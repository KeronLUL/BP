namespace WebScraper.Json.Entities;

public class CommandConfig
{
    public string? Name { get; set; }
    public IDictionary<string, string>? Args { get; set; }
}