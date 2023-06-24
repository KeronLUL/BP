namespace WebApp.Models;

public class CommandConfig
{
    public string? name { get; set; }
    public IDictionary<string, object>? args { get; set; }
}