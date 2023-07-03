using System;
using System.Collections.Generic;
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
    
    public async Task<List<ElementEntity>> GetAsync()
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<ElementEntity> query = uow.GetRepository<ElementEntity, ElementEntityMapper>().Get();
        List<ElementEntity> list = query.ToList();
        return list;
    }
}