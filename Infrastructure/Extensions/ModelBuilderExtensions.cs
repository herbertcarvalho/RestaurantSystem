using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Extensions;

public static class ModelBuilderExtensions
{
    public static void ToSnakeCaseNames(this ModelBuilder modelBuilder)
    {
        foreach (var entity in modelBuilder.Model.GetEntityTypes())
        {
            entity.SetTableName(entity.GetTableName().ToSnakeCase());
            foreach (var property in entity.GetProperties())
            {
                property.SetColumnName(property.GetColumnName().ToSnakeCase());
            }
        }
    }

}