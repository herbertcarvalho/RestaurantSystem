using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

internal class RestaurantReviewMap : IEntityTypeConfiguration<RestaurantReview>
{
    public void Configure(EntityTypeBuilder<RestaurantReview> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id)
               .ValueGeneratedOnAdd();

        builder.Property(x => x.Rating)
               .IsRequired();
        builder.Property(x => x.RestaurantId)
               .IsRequired();
        builder.Property(x => x.Comment)
               .IsRequired();
        builder.Property(x => x.Category)
               .IsRequired();

        builder.HasOne(x => x.Restaurant)
               .WithMany(x => x.Reviews)
               .HasForeignKey(x => x.RestaurantId);
    }
}

