using WebScraper.Database.Entities;
using WebScraper.Database.EntityCreator;
using WebScraper.Database.Facades;
using WebScraper.Json.Entities;

namespace WebScraper.EntityHandlers;

public static class CreateOrRetrieveWebsite
{
    public static WebsiteEntity CreateOrRetrieve(Config config)
    {
        var websiteFacade = new WebsiteFacade();
        
        var website = websiteFacade.GetAsync(config.url).Result;
        if (website == null)
        {
            website = WebsiteCreator.CreateWebsite(config!.url);
            websiteFacade.SaveAsync(website);
        }

        return website;
    }
}