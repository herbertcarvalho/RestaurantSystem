using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddPersistenceContexts(this IServiceCollection services,
           IConfiguration configuration)
    {
        services.AddTransient<IApplicationDbContext, ApplicationDbContext>();
        AddDbContext(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_POSTGRES") ?? configuration.GetConnectionString("ApplicationConnection");

        services.AddDbContext<ApplicationDbContext>(
             options => options
                .EnableSensitiveDataLogging()
                .UseNpgsql(configuration.GetConnectionString("ApplicationConnection"),
              b => b.MigrationsAssembly("RestaurantSystem.Api"))
         );
    }
}
