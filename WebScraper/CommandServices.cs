using System.Reflection;

namespace WebScraper;

public static class CommandServices
{
    public static List<string> GetCommandsNames()
    {
        var assembly = Assembly.GetExecutingAssembly().GetTypes();
        var commands = assembly.Where(name =>
                name.FullName!.Contains($@"WebScraper.SeleniumCommands"))
            .ToArray();
        return (from command in commands where command.IsPublic select command.Name).ToList();
    }

    public static PropertyInfo[] GetProperties(string? commandName)
    {
        var assembly = Assembly.GetExecutingAssembly().GetTypes();
        var command = assembly.Where(name =>
            name.FullName!.Contains($@"WebScraper.SeleniumCommands.{commandName}")).ToArray()[0];
        
        return command.GetProperties();
    }
}