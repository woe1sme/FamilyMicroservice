using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Family.Infrastructure.EntityFramework.Configuration;

public class FamilyEntityTypeConfiguration 
    : IEntityTypeConfiguration<Domain.Entities.Family>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Family> builder)
    {
        builder.HasKey("Id");
        builder.Property("FamilyName")
            .IsRequired()
            .HasMaxLength(200);
        builder.HasMany(f => f.FamilyMembers)
            .WithOne(fm => fm.Family)
            .HasForeignKey(fm => fm.FamilyId);
    }
}