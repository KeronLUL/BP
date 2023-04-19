using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebScraper.Database.Entities;

namespace WebScraper.Database.Facades;

public class ElementFacade
{
    public async Task<ElementEntity?> GetAsync(string? name, Guid websiteId)
    {
        try
        {
            var ctx = new WebScraperDbContext();
            var entity = await ctx.Elements.FirstOrDefaultAsync(i => i.Name == name && i.WebsiteId == websiteId );
            return entity;
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Retrieving entity failed.");
        }
    }

    public async void SaveAsync(ElementEntity entity)
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
    
    public async void UpdateAsync(ElementEntity entity)
    {
        try
        {
            var ctx = new WebScraperDbContext();
            ctx.Update(entity);
            await ctx.SaveChangesAsync();
        }
        catch (DbUpdateException)
        {
            throw new InvalidOperationException("Updating entity failed.");
        }

    }
}