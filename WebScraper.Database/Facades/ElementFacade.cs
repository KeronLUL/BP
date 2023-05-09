using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebScraper.Database.Entities;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Mappers;
using WebScraper.Database.UnitOfWork;

namespace WebScraper.Database.Facades;

public class ElementFacade :
    FacadeBase<ElementEntity, ElementEntityMapper>, IElementFacade
{
    public ElementFacade(
        IUnitOfWorkFactory unitOfWorkFactory)
        : base(unitOfWorkFactory)
    {
    }
    
    public async Task<ElementEntity?> GetAsync(string? name, Guid websiteId)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ElementEntity> query = uow.GetRepository<ElementEntity, ElementEntityMapper>().Get();
        ElementEntity? entity = await query.SingleOrDefaultAsync(e => e.Name == name && e.WebsiteId == websiteId);
        
        return entity;
    }
}