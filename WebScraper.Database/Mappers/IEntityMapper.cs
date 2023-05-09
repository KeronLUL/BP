using WebScraper.Database.Entities;

namespace WebScraper.Database.Mappers;

public interface IEntityMapper<in TEntity>
    where TEntity : IEntity
{
    void MapToExistingEntity(TEntity existingEntity, TEntity newEntity);
}
