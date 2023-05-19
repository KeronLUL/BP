using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;

namespace WebScraper.Json;

public static class JsonValidator
{
    public static bool Validate(string pathToSchema, string pathToConfig, ILogger logger)
    {
        logger.LogInformation("Validating JSON schema...");

        try
        {
            var config = File.ReadAllText(pathToConfig);
            var jsonConfig = JToken.Parse(config);

            var schema = File.ReadAllText(pathToSchema);
            var jsonSchema = JSchema.Parse(schema);

            logger.LogInformation("Done validating JSON schema.");
        
            return jsonConfig.IsValid(jsonSchema);
        }
        catch (Exception)
        {
            logger.LogError("Parsing of JSON config has failed");
            return false;
        }
    }
}