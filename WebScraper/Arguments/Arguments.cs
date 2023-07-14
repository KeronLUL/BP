using CommandLine;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace WebScraper.Arguments;

public static class Argument
{
    private static string _filename = "";
    private static bool _headless;
    private static bool _maximized;

    public static void ParseArguments(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                if (o.Filename != null)
                {
                    if (File.Exists(o.Filename))
                    {
                        _filename = o.Filename;
                    }
                }

                if (o.Headless) _headless = true;
                if (o.Maximized) _maximized = true;
            });
    }

    public static string GetFilename()
    {
        return _filename;
    }
    
    public static bool Headless()
    {
        return _headless;
    }
    
    public static bool Maximized()
    {
        return _maximized;
    }
}