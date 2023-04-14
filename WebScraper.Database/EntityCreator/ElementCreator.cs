using System;
using WebScraper.Database.Entities;

namespace WebScraper.Database.EntityCreator;

public static class ElementCreator
{
    public static ElementEntity CreateElement(string? name, string? value, Guid websideId)
    {
        var element = new ElementEntity()
        {
            Id = Guid.NewGuid(),
            Name = name,
            Value = value,
            WebsiteId = websideId
        };
        return element;
    }
}