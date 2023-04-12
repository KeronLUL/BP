using Newtonsoft.Json;
using WebScraper.Json;
using WebScraper.Arguments;
using WebScraper.Json.Entities;

namespace WebScraper
{
    internal abstract class Program
    {
        private static int Main(string[] args)
        {
            try
            {
                Args.ParseArguments(args);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }

            if (!File.Exists("WebScraper/config.schema.json"))
            {
                JsonGenerator.GenerateJson("WebScraper/config.schema.json");
            }
            
            if (!JsonValidator.Validate("WebScraper/config.schema.json", Args.GetFilename()))
            {
                Console.WriteLine("Config file is not valid");
                return 1;
            }

            Config? config;
            try
            {
                config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Args.GetFilename()));
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }

            try
            {
                Scraper.Run(config);
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);
                return 1;
            }

            return 0;
        }
    }
}