using System;
using WebScraper.Database.Entities;

namespace WebScraper.Database.EntityCreator;

public static class WebsiteCreator
{
    public static WebsiteEntity CreateWebsite(string? url)
    {
        var element = new WebsiteEntity()
        {
            Id = Guid.NewGuid(),
            URL = url
        };
        return element;
    }
}