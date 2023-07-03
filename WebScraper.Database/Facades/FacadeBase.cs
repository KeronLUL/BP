using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
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
    
    public async Task DeleteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        try
        {
            uow.GetRepository<TEntity, TEntityMapper>().Delete(id);
            await uow.CommitAsync().ConfigureAwait(false);
        }
        catch (DbUpdateException e)
        {
            throw new InvalidOperationException("Entity deletion failed.", e);
        }
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