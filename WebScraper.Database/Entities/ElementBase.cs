using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WebScraper.Database.Entities;

public abstract record EntityBase : IEntity
{
    public Guid Id { get; set; }
}