using Microsoft.EntityFrameworkCore;

namespace WebScraper.Database.UnitOfWork;

public class UnitOfWorkFactory : IUnitOfWorkFactory
{
    private readonly IDbContextFactory<WebScraperDbContext> _dbContextFactory;

    public UnitOfWorkFactory(IDbContextFactory<WebScraperDbContext> dbContextFactory) =>
        _dbContextFactory = dbContextFactory;

    public IUnitOfWork Create() => new UnitOfWork(_dbContextFactory.CreateDbContext());
}
