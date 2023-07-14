using CommandLine;
using CommandLine.Text;

namespace WebScraper.Arguments;

public class Options
{
    [Option('f', "filename", Required = true, HelpText = "Config filename.")]
    public string? Filename { get; set; }
    [Option('h', "headless", Required = false, HelpText = "Headless mode.")]
    public bool Headless { get; set; }
    [Option('m', "maximized", Required = false, HelpText = "Maximized mode. Works only with Chrome driver")]
    public bool Maximized { get; set; }

    [Usage(ApplicationAlias = "WebScraper")]
    public static IEnumerable<Example> Examples
    {
        get
        {
            return new List<Example>() {
                new Example("Automatic scrapping of website specified in config file", new Options { Filename = "file.json" }),
                new Example("Headless mode", new Options { Filename = "file.json", Headless = true }),
                new Example("Headless and maximized mode", new Options { Filename = "file.json", Headless = true, Maximized = true})
            };
        }
    }
}