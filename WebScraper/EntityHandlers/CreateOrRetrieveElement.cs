using WebScraper.Database.Entities;
using WebScraper.Database.EntityCreator;
using WebScraper.Database.Facades;
using WebScraper.Json.Entities;

namespace WebScraper.EntityHandlers;

public static class CreateOrRetrieveElement
{
    public static ElementEntity CreateOrRetrieve(WebsiteEntity website, string? name, string? value)
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

        return element;
    }
}