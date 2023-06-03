using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace WebScraper.Json;

public static class JsonGenerator
{
    private static string GetParameterType(PropertyInfo propertyInfo)
    {
        if (propertyInfo.PropertyType == typeof(string))
            return "string";
        if (propertyInfo.PropertyType == typeof(int))
            return "integer";
        return "";
    }

    private static void GenerateParameters(PropertyInfo[] parameters, List<string> propertyList, 
        IDictionary<string, object> argProperties)
    {
        foreach (var parameter in parameters)
        {
            IDictionary<string, object> arg = new Dictionary<string, object>();
            arg.Add("type", $@"{GetParameterType(parameter)}");
            if(GetParameterType(parameter) == "integer")
                arg.Add("minimum", 0);
            argProperties.Add($@"{parameter.Name.ToLower()}", arg);
            propertyList.Add(parameter.Name.ToLower());
        }
    }
    
    private static void GenerateCommandConfigs(List<object> commandList)
    {
        var assembly = Assembly.GetExecutingAssembly().GetTypes();
        var commands = assembly.Where(name =>
                name.FullName!.Contains($@"WebScraper.SeleniumCommands"))
            .ToArray();
        foreach (var command in commands) if (command.IsPublic)
        {
            IDictionary<string, object> internalCommand = new Dictionary<string, object>();
            IDictionary<string, object> properties = new Dictionary<string, object>();
            IDictionary<string, object> argProperties = new Dictionary<string, object>();
            IDictionary<string, object> name = new Dictionary<string, object>();
            IDictionary<string, object> args = new Dictionary<string, object>();
            
            var propertyList = new List<string>();
            GenerateParameters(command.GetProperties(), propertyList, argProperties);

            args.Add("type", "object");
            args.Add("properties", argProperties);
            args.Add("required", propertyList);
            
            name.Add("type", "string");
            name.Add("pattern", $@"^{command.Name}$");
            
            properties.Add("name", name);
            properties.Add("args", args);
            
            internalCommand.Add("type", "object");
            internalCommand.Add("properties", properties);

            commandList.Add(internalCommand);
        }
    }
    public static void GenerateJsonSchema(String path, ILogger logger)
    {
        logger.LogInformation("Generating JSON schema...");

        IDictionary<string, object> config = new Dictionary<string, object>();
        IDictionary<string, object> properties = new Dictionary<string, object>();
        IDictionary<string, string> url = new Dictionary<string, string>();
        IDictionary<string, string> loop = new Dictionary<string, string>();
        IDictionary<string, object> driver = new Dictionary<string, object>();
        IDictionary<string, object> waitTime = new Dictionary<string, object>();
        IDictionary<string, object> commands = new Dictionary<string, object>();
        IDictionary<string, object> items = new Dictionary<string, object>();

        var drivers = new List<string> //enum
        {
            "Chrome",
            "Firefox",
            "Safari"
        };
        
        var anyOf = new List<object>();

        url.Add("type", "string");
        url.Add("format", "uri");
        loop.Add("type", "boolean");
        driver.Add("type", "string");
        driver.Add("enum", drivers);
        waitTime.Add("type", "integer");
        waitTime.Add("minimum", 0);
        
        GenerateCommandConfigs(anyOf);

        items.Add("anyOf", anyOf);
        
        commands.Add("type", "array");
        commands.Add("items", items);
        
        properties.Add("url", url);
        properties.Add("loop", loop);
        properties.Add("driver", driver);
        properties.Add("waitTime", waitTime);
        properties.Add("commands", commands);

        config.Add("type", "object");
        config.Add("properties", properties);

        var requiredFields = new List<string>();
        requiredFields.AddRange(properties.Select(property  => property.Key )
                                                .Where(i => i != "driver" && i != "commands"));

        config.Add("required", requiredFields);

        using var sw = File.CreateText(path);
        sw.Write(JsonConvert.SerializeObject(config, Formatting.Indented));
        logger.LogInformation($@"Done generating JSON schema '{path}'.");
    }
}