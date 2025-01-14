using Family.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Family.Infrastructure.EntityFramework.Configuration;

public class FamilyMemberEntityTypeConfiguration
    : IEntityTypeConfiguration<Domain.Entities.FamilyMember>
{
    public void Configure(EntityTypeBuilder<FamilyMember> builder)
    {
        builder.Property("Name")
            .IsRequired()
            .HasMaxLength(200);
        
        //TODO configure
    }
}