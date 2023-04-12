using CommandLine;

namespace WebScraper.Arguments;

public static class Args
{
    private static string _filename = "";
    private static bool _verbose;

    public static void ParseArguments(string[] args)
    {
        Parser.Default.ParseArguments<Options>(args)
            .WithParsed(o =>
            {
                if (o.Filename != null)
                {
                    if (!File.Exists(o.Filename))
                    {
                        Console.Error.WriteLine($@"Invalid path to file {o.Filename}");
                        throw new FileNotFoundException();
                    }
                    _filename = o.Filename;
                    
                }

                if (o.Verbose)
                {
                    _verbose = true;
                }
            });
    }

    public static string GetFilename()
    {
        return _filename;
    }

    public static void PrintVerbose(string message)
    {
        if (_verbose)
        {
            Console.WriteLine(message);
        }
    }
}