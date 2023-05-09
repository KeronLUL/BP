using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebScraper.Database.Entities;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Mappers;
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
    
    public async Task<WebsiteEntity?> GetAsync(string? url)
    {
        await using IUnitOfWork uow = UnitOfWorkFactory.Create();
        IQueryable<WebsiteEntity> query = uow.GetRepository<WebsiteEntity, WebsiteEntityMapper>().Get();
        WebsiteEntity? entity = await query.SingleOrDefaultAsync(e => e.URL == url);
        
        return entity;
    }
}
