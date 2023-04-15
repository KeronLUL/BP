namespace WebScraper.Json.Entities;

public class Config
{
    public string? Url { get; set; }
    public bool Loop { get; set; }
    public string? Driver { get; set; }
    public int WaitTime { get; set; }
    public List<Command>? Commands { get; set; }
}