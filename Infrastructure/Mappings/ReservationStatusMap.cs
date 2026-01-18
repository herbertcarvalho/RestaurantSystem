using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

internal class ReservationStatusMap : IEntityTypeConfiguration<ReservationStatus>
{
    public void Configure(EntityTypeBuilder<ReservationStatus> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
               .HasMaxLength(200)
               .IsRequired();

        builder.HasData(
            new ReservationStatus { Id = 1, Name = "PENDING" },
            new ReservationStatus { Id = 2, Name = "CONFIRMED" },
            new ReservationStatus { Id = 3, Name = "CHECKED IN" },
            new ReservationStatus { Id = 4, Name = "REVIEW" },
            new ReservationStatus { Id = 5, Name = "COMPLETED" },
            new ReservationStatus { Id = 6, Name = "CANCELLED" },
            new ReservationStatus { Id = 7, Name = "NO SHOW" }
        );
    }
}
