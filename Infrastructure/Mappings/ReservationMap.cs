using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

internal class ReservationMap : IEntityTypeConfiguration<Reservation>
{
    public void Configure(EntityTypeBuilder<Reservation> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Status)
               .IsRequired();

        builder.Property(x => x.CustomerId)
               .IsRequired();

        builder.Property(x => x.RestaurantId)
               .IsRequired();

        builder.Property(x => x.NumberOfGuests)
               .IsRequired();

        builder.Property(x => x.Guid)
               .IsRequired();

        builder.Property(x => x.RequiresDeposit);

        builder.Property(x => x.SpecialRequests);

        builder.Property(x => x.DepositAmount);

        builder.Property(x => x.ReservationDate);

        builder.HasOne(x => x.Customer)
               .WithMany(x => x.Reservations)
               .HasForeignKey(x => x.CustomerId);

        builder.HasOne(x => x.Restaurant)
               .WithMany(x => x.Reservations)
               .HasForeignKey(x => x.RestaurantId);

    }
}
