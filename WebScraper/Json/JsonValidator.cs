﻿using Newtonsoft.Json.Linq;
using Newtonsoft.Json.Schema;
using WebScraper.Arguments;

namespace WebScraper.Json;

public static class JsonValidator
{
    public static bool Validate(string pathToSchema, string pathToConfig)
    {
        Args.PrintVerbose("Validating JSON schema...");

        try
        {
            var config = File.ReadAllText(pathToConfig);
            var jsonConfig = JToken.Parse(config);
        
            var schema = File.ReadAllText(pathToSchema);
            var jsonSchema = JSchema.Parse(schema);
            
            Args.PrintVerbose("Done validating JSON schema.");
        
            return jsonConfig.IsValid(jsonSchema);
        }
        catch (Exception)
        {
            Console.Error.WriteLine("Parsing of JSON config has failed");
            return false;
        }
    }
}