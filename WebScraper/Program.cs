using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WebScraper.Arguments;
using WebScraper.Database;
using WebScraper.Database.Facades;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Factories;
using WebScraper.Database.UnitOfWork;

namespace WebScraper
{
    public sealed class Program
    {
        public static async Task Main(string[] args)
        {
            Argument.ParseArguments(args);
            if (args.Contains("--help")) return;

            await Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration((hostingContext, config) =>
                {
                    config.AddCommandLine(args);
                })
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService<ScraperService>();
                    services.AddSingleton<IDbContextFactory<WebScraperDbContext>>(new DbContextSqLiteFactory(Paths.DbPath));
                    services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>(serviceProvider => 
                        new UnitOfWorkFactory(serviceProvider.GetRequiredService<IDbContextFactory<WebScraperDbContext>>()));
                    services.AddSingleton<IElementFacade>(provider =>
                        new ElementFacade(provider.GetRequiredService<IUnitOfWorkFactory>()));
                    services.AddSingleton<IWebsiteFacade>(provider =>
                        new WebsiteFacade(provider.GetRequiredService<IUnitOfWorkFactory>()));
                })
                .RunConsoleAsync();
        }
    }
}