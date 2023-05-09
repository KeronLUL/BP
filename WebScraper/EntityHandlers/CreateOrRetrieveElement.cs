using WebScraper.Database.Entities;
using WebScraper.Database.Facades;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Factories;
using WebScraper.Database.UnitOfWork;

namespace WebScraper.EntityHandlers;

public sealed class CreateOrUpdateElement
{
    private readonly IElementFacade _elementFacade;

    public CreateOrUpdateElement(
        IElementFacade elementFacade)
    {
        _elementFacade = elementFacade;
    }
    
    public async void CreateOrUpdate(WebsiteEntity website, string? name, string? value)
    {
        var result = _elementFacade.GetAsync(name, website.Id);
        
        if (result.Result == null)
        {
            var entity = new ElementEntity()
            {
                Id = Guid.NewGuid(),
                WebsiteId = website.Id,
                Name = name,
                Value = value
            };
            await _elementFacade.SaveAsync(entity);
        }
        else
        {
            result.Result.Value = value;
            await _elementFacade.SaveAsync(result.Result);
        }
    }
}