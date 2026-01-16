using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Infrastructure.Persistence;

public class DateTimeUtcInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(
        DbContextEventData eventData,
        InterceptionResult<int> result,
        CancellationToken ct = default)
    {
        if (eventData.Context is null) return ValueTask.FromResult(result);
        foreach (var entry in eventData.Context.ChangeTracker.Entries())
        {
            foreach (var property in entry.Properties)
            {
                if (property.Metadata.ClrType == typeof(DateTime) &&
                    property.CurrentValue is DateTime dt &&
                    dt.Kind != DateTimeKind.Utc)
                {
                    property.CurrentValue = DateTime.SpecifyKind(dt, DateTimeKind.Utc);
                }
            }
        }
        return ValueTask.FromResult(result);
    }
}
