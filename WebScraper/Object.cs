using System.Reflection;
using WebScraper.Json.Entities;

namespace WebScraper;

public static class Object
{
    public static bool CheckObject(Command command, PropertyInfo property)
    {
        switch (property.Name)
        {
            case "Click" when command.Click == null:
            case "SaveText" when command.SaveText == null:
            case "ImplicitWait" when command.ImplicitWait == null:
            case "SendKeys" when command.SendKeys == null:
            case "Submit" when command.Submit == null:
            case "Clear" when command.Clear == null:
            case "SaveAttribute" when command.SaveAttribute == null:
            case "Navigate" when command.Navigate == null:
            case "WaitUntilExists" when command.WaitUntilExists == null:
            case "WaitUntilClickable" when command.WaitUntilClickable == null:
            case "MoveToElement" when command.MoveToElement == null:
                return false;
            default:
                return true;
        }
    }

    public static string? GetParameter(Command command, PropertyInfo property, ParameterInfo parameter)
    {
        if (property.Name == "Click")
            return command.Click?.Args?.Path;
        if (property.Name == "ImplicitWait")
            return command.ImplicitWait?.Args?.Time.ToString();
        if (property.Name == "Clear")
            return command.Clear?.Args?.Path;
        if (property.Name == "Submit")
            return command.SaveText?.Args?.Path;
        if (property.Name == "Navigate")
            return command.Navigate?.Args?.Path;
        if (property.Name == "MoveToElement")
            return command.MoveToElement?.Args?.Path;
        if (property.Name == "SaveText")
            return parameter.Name == "path" ? command.SaveText?.Args?.Path : command.SaveText?.Args?.Name;
        if (property.Name == "SendKeys")
            return parameter.Name == "path" ? command.SendKeys?.Args?.Path : command.SendKeys?.Args?.Text;
        if (property.Name == "WaitUntilExists")
            return parameter.Name == "path" ? command.WaitUntilExists?.Args?.Path : command.WaitUntilExists?.Args?.Time.ToString();
        if (property.Name == "WaitUntilClickable")
            return parameter.Name == "path" ? command.WaitUntilClickable?.Args?.Path : command.WaitUntilClickable?.Args?.Time.ToString();
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