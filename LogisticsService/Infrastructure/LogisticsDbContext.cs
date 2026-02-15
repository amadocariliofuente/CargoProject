using Microsoft.EntityFrameworkCore;
using LogisticsService.Domain.Entities;

namespace LogisticsService.Infrastructure;

public class LogisticsDbContext : DbContext
{
    public LogisticsDbContext(DbContextOptions<LogisticsDbContext> options) : base(options)
    {
    }
    
    public DbSet<Loads> Loads { get; set; }
    
    public DbSet<Vehicles> Vehicles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Loads>()
            .Property(u => u.CargoType)
            .HasConversion<string>();
        
        modelBuilder.Entity<Loads>()
            .Property(u => u.LoadStatus)
            .HasConversion<string>();
        
        modelBuilder.Entity<Loads>()
            .Property(u => u.VehicleType)
            .HasConversion<string>();

        modelBuilder.Entity<Loads>()
            .Property(l => l.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Vehicles>()
            .Property(v => v.Id)
            .ValueGeneratedOnAdd();
        
        modelBuilder.Entity<Vehicles>()
            .Property(v => v.VehicleType)
            .HasConversion<string>();
    }
}