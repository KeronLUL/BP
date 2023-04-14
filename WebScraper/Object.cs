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
            return command.Click?.args?[0].path;
        if (property.Name == "SaveText")
            return parameter.Name == "path" ? command.SaveText?.args?[0].path : command.SaveText?.args?[0].name;
        if (property.Name == "ImplicitWait")
            return command.ImplicitWait?.args?[0].time.ToString();
        if (property.Name == "Clear")
            return command.Clear?.args?[0].path;
        if (property.Name == "SendKeys")
            return parameter.Name == "path" ? command.SendKeys?.args?[0].path : command.SendKeys?.args?[0].text;
        if (property.Name == "Submit")
            return command.SaveText?.args?[0].path;
        if (property.Name == "SaveAttribute")
            return parameter.Name switch
            {
                "path" => command.SaveAttribute?.args?[0].path,
                "attribute" => command.SaveAttribute?.args?[0].attribute,
                _ => command.SaveAttribute?.args?[0].name
            };
        return "";
    }
}