using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Facades.Interfaces;

public interface IElementFacade : IFacade<ElementEntity>
{
    Task<List<ElementEntity>> GetAsync();
}
