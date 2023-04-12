using System.Reflection;
using WebScraper.Json.Entities;

namespace WebScraper;

public static class Object
{
    public static bool CheckObject(Command command, PropertyInfo property)
    {
        if(property.Name == "Click" && command.Click == null)
            return false;
        if(property.Name == "Text" && command.Text == null)
            return false;
        
        return true;
    }

    public static string? GetParameter(Command command, PropertyInfo property, ParameterInfo parameter)
    {
        if (property.Name == "Click")
            return command.Click?.args?[0].path;
        if (property.Name == "Text")
            return command.Text?.args?[0].path;
        return "";
    }
}