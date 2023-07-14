using Microsoft.EntityFrameworkCore;
using WebScraper.Database;
using WebScraper.Database.Facades;
using WebScraper.Database.Facades.Interfaces;
using WebScraper.Database.Factories;
using WebScraper.Database.UnitOfWork;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddMudServices();
var databaseName = builder.Configuration.GetSection("ConnectionStrings").GetSection("WebScraperDb").Value;
if (databaseName != null)
    builder.Services.AddSingleton<IDbContextFactory<WebScraperDbContext>>(
        new DbContextSqLiteFactory(databaseName));
builder.Services.AddSingleton<IUnitOfWorkFactory, UnitOfWorkFactory>(serviceProvider => 
    new UnitOfWorkFactory(serviceProvider.GetRequiredService<IDbContextFactory<WebScraperDbContext>>()));
builder.Services.AddSingleton<IElementFacade>(provider =>
    new ElementFacade(provider.GetRequiredService<IUnitOfWorkFactory>()));
builder.Services.AddSingleton<IWebsiteFacade>(provider =>
    new WebsiteFacade(provider.GetRequiredService<IUnitOfWorkFactory>()));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();