using System.ComponentModel;
using Chatter.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Chatter.Infrastructure;

public class ChatterDbContext(DbContextOptions options)
    : IdentityDbContext<User, Role, Guid>(options)
{
    public DbSet<Message> Messages { get; set; }

    
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        base.ConfigureConventions(configurationBuilder);
        configurationBuilder.Properties<DateOnly>()
            .HaveConversion<DateOnlyConverter>();
        configurationBuilder.Properties<TimeOnly>()
            .HaveConversion<TimeOnlyConverter>();
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>()
            .HasData(
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                },
                new Role()
                {
                    Id = Guid.NewGuid(),
                    Name = "User",
                    NormalizedName = "USER"
                });
    }
}