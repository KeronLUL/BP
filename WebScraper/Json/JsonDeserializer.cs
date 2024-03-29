﻿using System.Reflection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using WebScraper.Arguments;
using WebScraper.Json.Entities;

namespace WebScraper.Json;

public static class JsonDeserializer
{
    public static Config? Deserialize(ref List<object> list, ILogger logger)
    {
        logger.LogInformation("Deserializing config...");
        var config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(Argument.GetFilename()));

        for (var index = 0; index < config!.Commands!.Count; index++)
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes();
            var commandName = config.Commands[index].Name;
            var classObject = assembly.Where(name =>
                    name.FullName == $@"WebScraper.SeleniumCommands.{commandName}")
                .ToArray()[0];

            var command = Activator.CreateInstance(classObject);
            foreach (var property in classObject.GetProperties())
            {
                foreach (var arg in config.Commands[index].Args!)
                {
                    if (property.Name.Equals(arg.Key, StringComparison.OrdinalIgnoreCase))
                    {
                        property.SetValue(command, arg.Value is long ? Convert.ToInt32(arg.Value) : arg.Value);
                    }
                }
            }
            list.Add(command!);
        }
        logger.LogInformation("Deserializing done.");
        return config;
    }
}