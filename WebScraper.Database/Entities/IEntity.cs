using System;

namespace WebScraper.Database.Entities;

public interface IEntity
{
    Guid Id { get; set; }
}