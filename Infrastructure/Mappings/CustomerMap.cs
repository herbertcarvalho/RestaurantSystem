using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Mappings;

internal class CustomerMap : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).ValueGeneratedOnAdd();

        builder.Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Email)
            .HasMaxLength(200)
            .IsRequired();

        builder.Property(x => x.Phone)
            .HasMaxLength(11)
            .IsRequired();

        builder.HasMany(x => x.Reservations)
               .WithOne(x => x.Customer)
               .HasForeignKey(x => x.CustomerId);

        var customers = new List<Customer>();
        for (int i = 1; i <= 51; i++)
        {
            customers.Add(new Customer
            {
                Id = i,
                Name = $"Customer {i}",
                Email = $"customer{i}@example.com",
                Phone = (10000000000 + i).ToString() // ensures 11-digit phone numbers
            });
        }

        builder.HasData(customers);
    }
}
