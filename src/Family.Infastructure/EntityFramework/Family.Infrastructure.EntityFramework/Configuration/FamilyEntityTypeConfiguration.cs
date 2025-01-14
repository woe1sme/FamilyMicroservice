using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Family.Infrastructure.EntityFramework.Configuration;

public class FamilyEntityTypeConfiguration 
    : IEntityTypeConfiguration<Domain.Entities.Family>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Family> builder)
    {
        builder.Property("FamilyName")
            .HasMaxLength(200);
        
        //TODO configure
    }
}