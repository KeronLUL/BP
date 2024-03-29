﻿using Microsoft.EntityFrameworkCore;
using WebScraper.Database.Entities;

namespace WebScraper.Database;

public class WebScraperDbContext : DbContext
{
    public DbSet<WebsiteEntity> Websites => Set<WebsiteEntity>();
    public DbSet<ElementEntity> Elements => Set<ElementEntity>();

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(@"Data Source=../WebScraper/Data/webscraper.db");
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<WebsiteEntity>()
            .HasMany<ElementEntity>(i => i.Elements)
            .WithOne(i => i.Website)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<WebsiteEntity>()
            .HasIndex(i => i.URL)  
            .IsUnique();
    }
}