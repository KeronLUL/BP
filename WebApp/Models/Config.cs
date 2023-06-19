using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security;

namespace WebApp.Models;

public class Config
{
    [Required]
    [Url]
    public string? url { get; set; }
    [DefaultValue(false)]
    public bool loop { get; set; }
    public string? driver { get; set; }
    [Required]
    [Range(0, 100000)]
    public int waitTime { get; set; }
    //public List<CommandConfig>? Commands { get; set; }
    
}