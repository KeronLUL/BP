using System.Threading.Tasks;
using WebScraper.Database.Entities;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Mappers;
using WebScraper.Database.Repositories;
using WebScraper.Database.UnitOfWork;

namespace WebScraper.Database.Facades;

public abstract class
    FacadeBase<TEntity, TEntityMapper> : IFacade<TEntity>
    where TEntity : class, IEntity
    where TEntityMapper : IEntityMapper<TEntity>, new()
{
    protected readonly IUnitOfWorkFactory UnitOfWorkFactory;

    protected FacadeBase(
        IUnitOfWorkFactory unitOfWorkFactory)
    {
        UnitOfWorkFactory = unitOfWorkFactory;
    }

    public virtual async Task<TEntity> SaveAsync(TEntity entity)
    {
        TEntity result;

        IUnitOfWork uow = UnitOfWorkFactory.Create();
        IRepository<TEntity> repository = uow.GetRepository<TEntity, TEntityMapper>();

        if (await repository.ExistsAsync(entity))
        {
            result = await repository.UpdateAsync(entity);
        }
        else
        {
            result = await repository.InsertAsync(entity);
        }

        await uow.CommitAsync();

        return result;
    }
}