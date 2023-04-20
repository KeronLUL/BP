using System.Reflection;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Schema;
using Newtonsoft.Json.Schema.Generation;
using Newtonsoft.Json.Serialization;
using WebScraper.Arguments;
using WebScraper.Json.Entities;

namespace WebScraper.Json;

public static class JsonGenerator
{
    public static void GenerateJson(String path)
    {
        Args.PrintVerbose("Generating JSON schema...");
        var generator = new JSchemaGenerator()
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver(),
            SchemaIdGenerationHandling = SchemaIdGenerationHandling.TypeName,
            DefaultRequired = Required.DisallowNull,
            SchemaReferenceHandling = SchemaReferenceHandling.None,
        };

        var schema = generator.Generate(typeof(Config));
        var jsonString = schema.ToString().Replace(@"""properties"":", @"""additionalProperties"": false, ""properties"":");
        
        using StreamWriter sw = File.CreateText(path);
        sw.Write(jsonString);
        Args.PrintVerbose($@"Done generating JSON schema '{path}'.");
    }
}