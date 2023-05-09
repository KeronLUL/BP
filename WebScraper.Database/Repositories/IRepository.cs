using System;
using System.Linq;
using System.Threading.Tasks;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Repositories;

public interface IRepository<TEntity>
    where TEntity : class, IEntity
{
    IQueryable<TEntity> Get();
    ValueTask<bool> ExistsAsync(TEntity entity);
    Task<TEntity> InsertAsync(TEntity entity);
    Task<TEntity> UpdateAsync(TEntity entity);
}
