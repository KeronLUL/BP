namespace WebScraper.Database.UnitOfWork;

public interface IUnitOfWorkFactory
{
    IUnitOfWork Create();
}
