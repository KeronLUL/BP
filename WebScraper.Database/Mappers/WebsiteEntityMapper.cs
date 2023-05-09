using WebScraper.Database.Entities;

namespace WebScraper.Database.Mappers;

public class WebsiteEntityMapper : IEntityMapper<WebsiteEntity>
{
    public void MapToExistingEntity(WebsiteEntity existingEntity, WebsiteEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.URL = newEntity.URL;
    }
}