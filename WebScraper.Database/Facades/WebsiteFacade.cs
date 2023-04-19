using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Facades;

public class WebsiteFacade
{
    public async Task<WebsiteEntity?> GetAsync(string? url)
    {
        try
        {
            var ctx = new WebScraperDbContext();
            var entity = await ctx.Websites.FirstOrDefaultAsync( i => i.URL == url);
            return entity;
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Retrieving entity failed.");
        }
    }

    public async void SaveAsync(WebsiteEntity entity)
    {
        try
        {
            var ctx = new WebScraperDbContext();
            ctx.Add(entity);
            await ctx.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Saving entity failed.");
        }
    }
}
