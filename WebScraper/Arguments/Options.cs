using CommandLine;
using CommandLine.Text;

namespace WebScraper.Arguments;

public class Options
{
    [Option('f', "filename", Required = true, HelpText = "Config filename.")]
    public string? Filename { get; set; }
            
    [Option('v', "verbose", Required = false, HelpText = "Verbose mode")]
    public bool Verbose { get; set; }

    [Usage(ApplicationAlias = "scraper")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            return new List<Example>() {
                new Example("Automatic scrapping of website specified in config file", new Options { Filename = "file.json" })
            };
        }
    }
}