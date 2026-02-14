using Microsoft.EntityFrameworkCore;
using LogisticsService.Domain.Entities;

namespace LogisticsService.Infrastructure;

public class LogisticsDbContext : DbContext
{
    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
    {
    }
    
    public DbSet<Loads> Loads { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loads>()
            .Property(u => u.CargoType)
            .HasConversion<string>();
        
        modelBuilder.Entity<Loads>()
            .Property(u => u.LoadStatus)
            .HasConversion<string>();
    }
}