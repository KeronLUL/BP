namespace WebScraper.Json.Entities;

public class Config
{
    public string? url { get; set; }
    public bool loop { get; set; }
    public string? driver { get; set; }
    public List<Command>? commands { get; set; }
}