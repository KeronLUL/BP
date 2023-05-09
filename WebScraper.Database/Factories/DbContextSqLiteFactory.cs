using Microsoft.EntityFrameworkCore;

namespace WebScraper.Database.Factories;

public class DbContextSqLiteFactory : IDbContextFactory<WebScraperDbContext>
{
    private readonly DbContextOptionsBuilder<WebScraperDbContext> _contextOptionsBuilder = new();

    public DbContextSqLiteFactory(string databaseName)
    {
        _contextOptionsBuilder.UseSqlite($"Data Source={databaseName};Cache=Shared");
    }

    public WebScraperDbContext CreateDbContext() => new();
}