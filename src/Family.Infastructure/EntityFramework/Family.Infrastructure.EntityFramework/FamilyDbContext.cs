using Family.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Family.Infrastructure.EntityFramework;

public class FamilyDbContext(DbContextOptions<FamilyDbContext> options) : DbContext(options)
{
    public DbSet<Domain.Entities.Family> Family { get; set; }
    public DbSet<FamilyMember> FamilyMember { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}