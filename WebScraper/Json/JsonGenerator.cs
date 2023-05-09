using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using WebScraper.Arguments;
using WebScraper.Json.Entities;

namespace WebScraper.Json;

public static class JsonGenerator
{
    public static void GenerateJson(String path, ILogger logger)
    {
        logger.LogInformation("Generating JSON schema...");
        var generator = new JSchemaGenerator()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            SchemaIdGenerationHandling = SchemaIdGenerationHandling.TypeName,
            DefaultRequired = Required.DisallowNull,
            SchemaReferenceHandling = SchemaReferenceHandling.Arrays,
        };

        var schema = generator.Generate(typeof(Config));

        using StreamWriter sw = File.CreateText(path);
        sw.Write(schema);
        logger.LogInformation($@"Done generating JSON schema '{path}'.");
    }
}