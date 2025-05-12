using EventServiceProvider.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace EventServiceProvider.Data.Contexts;

public class DataContext(DbContextOptions<DataContext> options) : DbContext(options)
{
    public DbSet<EventEntity> Events { get; set; }
}
