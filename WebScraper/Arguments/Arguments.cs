using CommandLine;
using Microsoft.Extensions.Logging;

namespace WebScraper.Arguments;

public static class Argument
{
    private static string _filename = "";

    public static void ParseArguments(string[] args, ILogger logger)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                if (o.Filename != null)
                {
                    if (!File.Exists(o.Filename))
                    {
                        logger.LogError($@"Invalid path to file {o.Filename}");
                        throw new FileNotFoundException();
                    }
                    _filename = o.Filename;
                }
            });
    }

    public static string GetFilename()
    {
        return _filename;
    }
}