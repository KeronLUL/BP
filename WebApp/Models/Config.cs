using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace WebApp.Models;

public class Config
{
    [Required]
    [Url]
    [Display(Name = "URL")]
    [DataType(DataType.Text)]
    public string? url { get; set; }
    [DefaultValue(false)]
    [Display(Name = "Loop")]
    public bool loop { get; set; }
    [Display(Name = "Select a driver")]
    [DataType("DropdownList")]
    public string? driver { get; set; }
    [Required]
    [Range(0, 10000000000000)]
    [Display(Name = "WaitTime")]
    public int waitTime { get; set; }
    public List<CommandConfig>? commands { get; set; }
}