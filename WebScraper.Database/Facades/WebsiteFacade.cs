using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebScraper.Database.Entities;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Mappers;
using WebScraper.Database.Repositories;
using WebScraper.Database.UnitOfWork;

namespace WebScraper.Database.Facades;

public class WebsiteFacade :
    FacadeBase<WebsiteEntity, WebsiteEntityMapper>, IWebsiteFacade
{
    public WebsiteFacade(
        IUnitOfWorkFactory unitOfWorkFactory)
        : base(unitOfWorkFactory)
    {
    }

    public async Task<WebsiteEntity> SaveWebsiteAsync(string? url)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<WebsiteEntity> query = uow.GetRepository<WebsiteEntity, WebsiteEntityMapper>().Get();
        IRepository<WebsiteEntity> repository = uow.GetRepository<WebsiteEntity, WebsiteEntityMapper>();
        WebsiteEntity? entity = await query.SingleOrDefaultAsync(e => e.URL == url);

        if (entity == null)
        {
            entity = new WebsiteEntity()
            {
                Id = Guid.NewGuid(),
                URL = url
            };
            await repository.InsertAsync(entity);
            await uow.CommitAsync();
            return entity;
        }
        return entity;
    }

    public async Task<WebsiteEntity?> GetWebsiteAsync(Guid id)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<WebsiteEntity> query = uow.GetRepository<WebsiteEntity, WebsiteEntityMapper>().Get();
        WebsiteEntity? entity = await query.SingleOrDefaultAsync(e => e.Id == id);
        
        return entity;
    }

    public async Task<List<WebsiteEntity>> GetAll()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<WebsiteEntity> query = uow.GetRepository<WebsiteEntity, WebsiteEntityMapper>().Get();
        List<WebsiteEntity> list = query.ToList();
        return list;
    }
}
