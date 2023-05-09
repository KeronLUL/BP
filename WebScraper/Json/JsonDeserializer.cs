using System.Reflection;
using Newtonsoft.Json;
using WebScraper.Json.Entities;

namespace WebScraper.Json;

public static class JsonDeserializer
{
    public static void Deserialize(ref List<object> list, Config? config)
    {
        for (int index = 0; index < config!.Commands!.Count; index++)
        {
            var assembly = Assembly.GetExecutingAssembly().GetTypes();
            var commandName = config.Commands[index].Name;
            var classObject = assembly.Where(name =>
                    name.FullName == $@"WebScraper.SeleniumCommands.{commandName}")
                .ToArray()[0];

            var command = Activator.CreateInstance(classObject);
            var arguments = JsonConvert.DeserializeObject<Args>(JsonConvert.SerializeObject(config.Commands[index].Args));
            foreach (var property in classObject.GetProperties())
            {
                foreach (var arg in arguments!.GetType().GetProperties())
                {
                    if (property.Name != arg.Name) continue;
                    property.SetValue(command, arg.GetValue(arguments));
                }
            }
            list.Add(command!);
        }
    }
}