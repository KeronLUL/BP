using System;

namespace WebScraper.Database.Entities;

public record ElementEntity : IEntity
{
    public required Guid WebsiteId { get; set; }
    
    public required string? Name { get; set; }
    public required string? Value { get; set; }
    public required string? Timestamp { get; set; }
    public WebsiteEntity? Website { get; set; }
    
    public required Guid Id { get; set; }
}