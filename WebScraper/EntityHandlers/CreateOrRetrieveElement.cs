using System.Reflection;
using WebScraper.Arguments;
using WebScraper.Database.Entities;
using WebScraper.Database.EntityCreator;
using WebScraper.Database.Facades;
using WebScraper.Json.Entities;

namespace WebScraper.EntityHandlers;

public static class CreateOrRetrieveElement
{
    public static void SaveValue(Command command, PropertyInfo commandProperty, WebsiteEntity website, string value)
    {
        if (commandProperty.Name == "SaveText")
        {
            Args.PrintVerbose($@"Saving text {commandProperty.Name}.");
            CreateOrRetrieve(website, command!.SaveText!.Args!.Name, value);
        }

        if (commandProperty.Name == "SaveAttribute")
        {
            Args.PrintVerbose($@"Saving attribute {command!.SaveAttribute!.Args!.Attribute}.");
            CreateOrRetrieve(website, command!.SaveAttribute!.Args!.Name, value);
        }
    }

    private static void CreateOrRetrieve(WebsiteEntity website, string? name, string? value)
    {
        var elementFacade = new ElementFacade();
        var element = elementFacade.GetAsync(name, website.Id).Result;
        
        if (element == null)
        {
            element = ElementCreator.CreateElement(name, value , website.Id);
            elementFacade.SaveAsync(element);
        }
        else
        {
            element.Value = value;
            elementFacade.UpdateAsync(element);
        }
    }
}