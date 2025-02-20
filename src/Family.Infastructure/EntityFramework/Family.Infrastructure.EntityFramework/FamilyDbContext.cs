using Family.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Family.Infrastructure.EntityFramework;

public sealed class FamilyDbContext : DbContext
{
    public DbSet<Domain.Entities.Family> Family { get; set; }
    public DbSet<FamilyMember> FamilyMember { get; set; }

    public FamilyDbContext(DbContextOptions<FamilyDbContext> options) : base(options)
    {
        //Database.EnsureDeleted();
        Database.EnsureCreated();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(GetType().Assembly);
    }
}