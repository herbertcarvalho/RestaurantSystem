using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

internal class RefreshTokenMap : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Token)
               .IsRequired();

        builder.Property(x => x.IsUsed)
               .IsRequired();

        builder.Property(x => x.ExpiresIn)
               .IsRequired();

        builder.HasIndex(r => r.Token).IsUnique();
    }
}
