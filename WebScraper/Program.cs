using Newtonsoft.Json;
using WebScraper.Json;
using WebScraper.Arguments;
using WebScraper.Json.Entities;

namespace WebScraper
{
    internal static class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                Args.ParseArguments(args);
            }
            catch (Exception)
            {
                Console.Error.WriteLine(@"Argument parsing failed");
                return ReturnCodes.ArgumentError;
            }

            if (!File.Exists(Paths.ProjectPath))
            {
                Console.Error.WriteLine("Program started from wrong directory. Please start this program from WebScraper directory");
                return ReturnCodes.ProjectPathError;
            }

            if (!File.Exists(Paths.ConfigPath))
            {
                JsonGenerator.GenerateJson(Paths.ConfigPath);
            }

            if (!JsonValidator.Validate(Paths.ConfigPath, Args.GetFilename()))
            {
                Console.Error.WriteLine("Config file is not valid");
                return ReturnCodes.ConfigError;
            }

            Config? config;
            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Args.GetFilename()));
                Args.PrintVerbose("Config file deserialized");
            }
            catch (Exception e)
            {
                Args.PrintVerbose(e.ToString());
                Console.Error.WriteLine($@"Failed to deserialize JSON from file: '{Args.GetFilename()}");
                return ReturnCodes.JsonError;
            }
            
            try
            {
                var scraper = new Scraper();
                Args.PrintVerbose("Running WebScraper...");
                scraper.Run(config);
            }
            catch (Exception)
            {
                Args.PrintVerbose("WebScraper finished with error.");
                return ReturnCodes.ScraperError;
            }
            Args.PrintVerbose("WebScraper finished.");
            
            return 0;
        }
    }
}