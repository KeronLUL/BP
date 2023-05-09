using WebScraper.Database.Entities;

namespace WebScraper.Database.Mappers;

public class ElementEntityMapper : IEntityMapper<ElementEntity>
{
    public void MapToExistingEntity(ElementEntity existingEntity, ElementEntity newEntity)
    {
        existingEntity.Id = newEntity.Id;
        existingEntity.WebsiteId = newEntity.WebsiteId;
        existingEntity.Name = newEntity.Name;
        existingEntity.Value = newEntity.Value;
    }
}