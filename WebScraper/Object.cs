using System.Reflection;
using WebScraper.Json.Entities;

namespace WebScraper;

public static class Object
{
    public static bool CheckObject(Command command, PropertyInfo property)
    {
        if(property.Name == "Click" && command.Click == null)
            return false;
        if(property.Name == "SaveText" && command.SaveText == null)
            return false;
        if(property.Name == "ImplicitWait" && command.ImplicitWait == null)
            return false;
        if(property.Name == "SendKeys" && command.SendKeys == null)
            return false;
        if(property.Name == "Submit" && command.Submit == null)
            return false;
        if(property.Name == "Clear" && command.Clear == null)
            return false;
        if(property.Name == "SaveAttribute" && command.SaveAttribute == null)
            return false;
        
        return true;
    }

    public static string? GetParameter(Command command, PropertyInfo property, ParameterInfo parameter)
    {
        if (property.Name == "Click")
            return command.Click?.Args?.Path;
        if (property.Name == "SaveText")
            return parameter.Name == "path" ? command.SaveText?.Args?.Path : command.SaveText?.Args?.Name;
        if (property.Name == "ImplicitWait")
            return command.ImplicitWait?.Args?.Time.ToString();
        if (property.Name == "Clear")
            return command.Clear?.Args?.Path;
        if (property.Name == "SendKeys")
            return parameter.Name == "path" ? command.SendKeys?.Args?.Path : command.SendKeys?.Args?.Text;
        if (property.Name == "Submit")
            return command.SaveText?.Args?.Path;
        if (property.Name == "SaveAttribute")
            return parameter.Name switch
            {
                "path" => command.SaveAttribute?.Args?.Path,
                "attribute" => command.SaveAttribute?.Args?.Attribute,
                _ => command.SaveAttribute?.Args?.Name
            };
        return "";
    }
}