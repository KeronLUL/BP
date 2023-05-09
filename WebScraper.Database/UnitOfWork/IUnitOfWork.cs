using System;
using System.Threading.Tasks;
using WebScraper.Database.Entities;
using WebScraper.Database.Mappers;
using WebScraper.Database.Repositories;

namespace WebScraper.Database.UnitOfWork;

public interface IUnitOfWork : IAsyncDisposable
{
    IRepository<TEntity> GetRepository<TEntity, TEntityMapper>()
        where TEntity : class, IEntity
        where TEntityMapper : IEntityMapper<TEntity>, new();

    Task CommitAsync();
}