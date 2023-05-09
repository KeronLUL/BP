using System;
using System.Threading.Tasks;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Facades.Interfaces;

public interface IElementFacade : IFacade<ElementEntity>
{
    Task<ElementEntity?> GetAsync(string? name, Guid websiteId);
}
