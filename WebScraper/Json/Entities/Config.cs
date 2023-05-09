using System.ComponentModel.DataAnnotations;
using Newtonsoft.Json;

namespace WebScraper.Json.Entities;

public class Config
{
    [Required, Url]
    public string? Url { get; set; }
    [Required]
    public bool Loop { get; set; }
    [EnumDataType(typeof(Drivers))]
    public string? Driver { get; set; }
    [Required, Range(typeof(decimal), "0", "1000000000000000000")]
    public int WaitTime { get; set; }
    public List<CommandConfig>? Commands { get; set; }
}