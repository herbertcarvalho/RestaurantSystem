using Domain.Extensions;
using Infrastructure.Extensions;
using Infrastructure.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace Infrastructure.DbContext;

public class ApplicationDbContext : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>, IApplicationDbContext
{

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public IDbConnection Connection => Database.GetDbConnection();

    public bool HasChanges => ChangeTracker.HasChanges();

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var listEntry = ChangeTracker.Entries<Entity>().ToList();

        return base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var property in modelBuilder.Model.GetEntityTypes()
        .SelectMany(t => t.GetProperties())
        .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
        {
            property.SetColumnType("decimal(18,2)");
        }

        modelBuilder.ToSnakeCaseNames();
        modelBuilder.ApplyUtcDateTimeConverter();
        modelBuilder.UseIdentityColumns();
        modelBuilder.ApplyConfiguration(new CustomerMap());
        modelBuilder.ApplyConfiguration(new ReservationMap());
        modelBuilder.ApplyConfiguration(new ReservationStatusMap());
        modelBuilder.ApplyConfiguration(new RestaurantMap());

        base.OnModelCreating(modelBuilder);
    }
}