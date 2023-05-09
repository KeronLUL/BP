using WebScraper.Database.Entities;
using WebScraper.Database.Facades;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Factories;
using WebScraper.Database.UnitOfWork;

namespace WebScraper.EntityHandlers;

public sealed class CreateOrRetrieveWebsite
{
    private readonly IWebsiteFacade _websiteFacade;

    public CreateOrRetrieveWebsite(
        IWebsiteFacade websiteFacade)
    {
        _websiteFacade = websiteFacade;
    }
    
    public async Task<WebsiteEntity> CreateOrRetrieve(string? url)
    {
        var result = _websiteFacade.GetAsync(url);
        
        if (result.Result == null)
        {
            var entity = new WebsiteEntity()
            {
                Id = Guid.NewGuid(),
                URL = url
            };
            await _websiteFacade.SaveAsync(entity);
            return entity;
        }

        return result.Result;
    }
}