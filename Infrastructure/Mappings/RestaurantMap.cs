using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

internal class RestaurantMap : IEntityTypeConfiguration<Restaurant>
{
    public void Configure(EntityTypeBuilder<Restaurant> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasMaxLength(200)
               .IsRequired();

        builder.HasData(
            new Restaurant() { Id = 1, Name = "Standart Restaurant" }
            );
    }
}
