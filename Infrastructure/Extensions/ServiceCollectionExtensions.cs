using Domain.Repositories;
using Infrastructure.DbContext;
using Infrastructure.Persistence;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                var interceptor = sp.GetRequiredService<DateTimeUtcInterceptor>();

                options
                    .EnableSensitiveDataLogging()
                    .UseNpgsql(
                        configuration.GetConnectionString("ApplicationConnection"),
                        b => b.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)
                    )
                    .UseSnakeCaseNamingConvention()
                    .ConfigureWarnings(w =>
                        w.Ignore(RelationalEventId.PendingModelChangesWarning))
                    .AddInterceptors(interceptor);
            });
    }

    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<DateTimeUtcInterceptor>();
    }

    public static void AddCustomIdentity(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDefaultIdentity<IdentityUser<int>>()
            .AddRoles<IdentityRole<int>>()
            .AddRoleManager<RoleManager<IdentityRole<int>>>()
            .AddRoleValidator<RoleValidator<IdentityRole<int>>>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();

        services.AddAuthentication(
            JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options =>
             options.TokenValidationParameters = new TokenValidationParameters
             {
                 ValidateIssuer = true,
                 ValidateAudience = true,
                 ValidateLifetime = true,
                 ValidAudience = configuration["TokenConfiguration:Audience"],
                 ValidIssuer = configuration["TokenConfiguration:Issuer"],
                 ValidateIssuerSigningKey = true,
                 IssuerSigningKey = new SymmetricSecurityKey(
                     Encoding.UTF8.GetBytes(configuration["Jwt:key"]))
             });
    }

    public static void AddRepositories(this IServiceCollection services)
    {
        services.AddTransient(typeof(IRepositoryAsync<>), typeof(RepositoryAsync<>));
        services.AddTransient<IUnitOfWork, UnitOfWork>();
        services.AddTransient<ICustomerRepository, CustomerRepository>();
        services.AddTransient<IReservationRepository, ReservationRepository>();
        services.AddTransient<IRestaurantRepository, RestaurantRepository>();
    }
}
