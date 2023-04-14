using System;
using System.Collections.Generic;

namespace WebScraper.Database.Entities;

public record WebsiteEntity : IEntity
{
    public required string? URL { get; set; }

    public ICollection<ElementEntity> Elements { get; set; } = new List<ElementEntity>();
    public required Guid Id { get; set; }
}