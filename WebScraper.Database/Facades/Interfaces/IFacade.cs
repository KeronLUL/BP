using System;
using System.Threading.Tasks;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Facades.Interfaces;

public interface IFacade<TEntity>
    where TEntity : class, IEntity
{
    Task DeleteAsync(Guid id);
    Task<TEntity> SaveAsync(TEntity entity);
}
