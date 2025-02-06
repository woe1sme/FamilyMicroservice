using Family.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Family.Infrastructure.EntityFramework.Configuration;

public class UserInfoEntityTypeConfiguration : IEntityTypeConfiguration<UserInfo>
{
    public void Configure(EntityTypeBuilder<UserInfo> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.UserName)
            .IsRequired()
            .HasMaxLength(200);
    }
}