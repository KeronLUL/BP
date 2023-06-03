using System;
using System.Threading.Tasks;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Facades.Interfaces;

public interface IWebsiteFacade : IFacade<WebsiteEntity>
{
    Task<WebsiteEntity> SaveWebsiteAsync(string? url);
}