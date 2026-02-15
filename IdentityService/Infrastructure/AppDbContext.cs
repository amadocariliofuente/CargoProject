using IdentityService.Domain.Entities;
using IdentityService.Domain.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace IdentityService.Infrastructure;

public class AppDbContext : IdentityDbContext<Users, IdentityRole<Guid>, Guid>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    // public DbSet<Users> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Users>()
            .Property(u => u.UserType)
            .HasConversion<string>();

        //modelBuilder.Ignore<IdentityPasskeyData>();
        //modelBuilder.Ignore(typeof(IdentityUserPasskey<Guid>));
        
        /*modelBuilder.Entity<Users>().HasData(
            new Users { Id = new Guid("11111111-1111-1111-1111-111111111111"), FirstName = "Admin", SecondName = "Admin", Email = "Admin@example.com", Age = 100, UserType = UserType.Admin, CreatedDate = new DateTime(2026, 2, 12)},    
            new Users { Id = new Guid("21111111-1111-1111-1111-111111111111"), FirstName = "Jack", SecondName = "Smith", Email = "Jack@example.com", Age = 25, UserType = UserType.Broker, CreatedDate = new DateTime(2026, 2, 12)},
            new Users { Id = new Guid("31111111-1111-1111-1111-111111111111"), FirstName = "Alice", SecondName = "Brown", Email = "Alice@example.com", Age = 30,UserType = UserType.Carrier, CreatedDate = new DateTime(2026, 2, 12)}
        );*/
        // Seed Data
    }
}