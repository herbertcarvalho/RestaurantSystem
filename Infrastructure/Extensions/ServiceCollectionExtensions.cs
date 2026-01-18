using Asp.Versioning;
using Domain.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.DbContext;
using Infrastructure.Jobs;
using Infrastructure.Persistence;
using Infrastructure.RabbitMQ.Consumers;
using Infrastructure.RabbitMQ.Publishers;
using Infrastructure.Repositories;
using Infrastructure.Services.Hubs;
using Infrastructure.Services.Notifier;
using Infrastructure.Services.ReservationServ;
using Infrastructure.Services.TokenService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Caching.Hybrid;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using System.Text;
using System.Threading.RateLimiting;

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
        services.AddScoped<IReservationService, ReservationService>();
        services.AddScoped<IRabbitMqPublisher, RabbitMqPublisher>();
        services.AddHostedService<RabbitMqConsumer>();
        services.AddScoped<ReservationNotifier>();
        services.AddScoped<TokenService>();
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
        services.AddTransient<IRefreshTokenRepository, RefreshTokenRepository>();
        services.AddTransient<IRestaurantReviewRepository, RestaurantReviewRepository>();
    }

    public static void AddHangfireServiceSignalR(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHangfire(config =>
            config.UsePostgreSqlStorage(configuration.GetConnectionString("ApplicationConnection"),
                new Hangfire.PostgreSql.PostgreSqlStorageOptions
                {
                    PrepareSchemaIfNecessary = true
                }));

        services.AddHangfireServer();
        services.AddSignalR();
    }

    public static void AddHybridCaching(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHybridCache(options =>
        {
            options.DefaultEntryOptions = new HybridCacheEntryOptions
            {
                Expiration = TimeSpan.FromMinutes(3),
                LocalCacheExpiration = TimeSpan.FromMinutes(1)
            };
        });
        services.AddStackExchangeRedisCache(options =>
        {
            options.Configuration = configuration.GetConnectionString("Redis");
        });
    }

    public static void AddJobs()
    {
        NoShowProcessingJob.Register();
    }

    public static void MapHubs(this WebApplication app)
    {
        app.MapHub<ReservationHub>("/hubs/reservations");
    }

    public static void AddRateLimit(this IServiceCollection services)
    {
        services.AddRateLimiter(options =>
        {
            options.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
            {
                var username = httpContext.User.Identity?.Name ?? "anonymous";

                return RateLimitPartition.GetFixedWindowLimiter(username, _ => new FixedWindowRateLimiterOptions
                {
                    PermitLimit = 100,
                    Window = TimeSpan.FromMinutes(1),
                    QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                    QueueLimit = 0
                });
            });

            options.OnRejected = async (context, cancellationToken) =>
            {
                context.HttpContext.Response.StatusCode = 429;
                await context.HttpContext.Response.WriteAsync("Too many requests. Try again later.", cancellationToken);
            };
        });
    }

    public static void AddVersioning(this IServiceCollection services)
    {

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Scheme = "Bearer",
                Description = "Input your Bearer token in this format - Bearer {your token here} to access this API",
                Type = SecuritySchemeType.ApiKey,
            });
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Restaurant System",
            });
        });

        services.AddApiVersioning(options =>
        {
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.ReportApiVersions = true;
            options.ApiVersionReader = new UrlSegmentApiVersionReader();
        });
    }

    public static void addSerilog(this WebApplicationBuilder webApplicationBuilder)
    {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Information()
        .Enrich.FromLogContext()
        .WriteTo.Console()
        .WriteTo.File("logs/booking-.txt", rollingInterval: RollingInterval.Day)
        .WriteTo.Seq(webApplicationBuilder.Configuration.GetConnectionString("Seq"))
        .CreateLogger();

        webApplicationBuilder.Host.UseSerilog();

        Serilog.Debugging.SelfLog.Enable(Console.Error);
    }

    public static void AddTelemetry(this IServiceCollection services)
    {
        //services.AddSingleton(new Meter("MyBookingService.Meter"));
        services.AddOpenTelemetry()
        .WithTracing(tracerProviderBuilder =>
        {
            tracerProviderBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyBookingService"))
                .AddAspNetCoreInstrumentation()
                 .AddConsoleExporter();
        })
        .WithMetrics(metricsBuilder =>
        {
            metricsBuilder
                .SetResourceBuilder(ResourceBuilder.CreateDefault().AddService("MyBookingService"))
                .AddAspNetCoreInstrumentation()
                .AddPrometheusExporter();
        });
    }
}
