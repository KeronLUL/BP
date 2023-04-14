using Newtonsoft.Json;
using SeleniumGenerated;
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
                return 1;
            }

            if (!File.Exists("config.schema.json"))
            {
                JsonGenerator.GenerateJson("config.schema.json");
            }

            if (!JsonValidator.Validate("config.schema.json", Args.GetFilename()))
            {
                Console.WriteLine("Config file is not valid");
                return 1;
            }

            Config? config;
            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Args.GetFilename()));
            }
            catch (Exception)
            {
                Console.Error.WriteLine($@"Failed to deserialize JSON from file: '{Args.GetFilename()}");
                return 1;
            }
            
            try
            {
                var scraper = new Scraper();
                scraper.Run(config);
            }
            catch (Exception)
            {
                SeleniumWebDriver.Quit();
                return 1;
            }
            
            return 1;
        }
    }
}