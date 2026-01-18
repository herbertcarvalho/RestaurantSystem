using Application.Extensions;
using Application.Interfaces;
using Domain.Events;
using Hangfire;
using Hangfire.PostgreSql;
using Infrastructure.DbContext;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using RestaurantSystem.Api.Middleware;
using ServiceCollectionExtensions = Infrastructure.Extensions.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddVersioning();
builder.Services.AddControllers();
builder.Services.AddTelemetry();
builder.addSerilog();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddPersistenceContexts(builder.Configuration);
builder.Services.AddRepositories();
builder.Services.AddCustomIdentity(builder.Configuration);
builder.Services.AddSwaggerGen();
builder.Services.AddDomainEvents();
builder.Services.AddServices();
builder.Services.AddCommands();
builder.Services.AddQueries();
builder.Services.AddHangfireServiceSignalR(builder.Configuration);
#region HealthConfig
string connectionString = builder.Configuration.GetConnectionString("ApplicationConnection");
string redisConnectionString = builder.Configuration.GetConnectionString("Redis");
string rabbitConnectionString = builder.Configuration.GetConnectionString("RabbitMQ");
var hangfireStorage = new PostgreSqlStorage(connectionString);

var connection = new ConnectionFactory
{
    Uri = new Uri(rabbitConnectionString)
};
var connect = await connection.CreateConnectionAsync();
#endregion
builder.Services.AddHealthChecks()
    .AddNpgSql(connectionString, name: "postgres")
    .AddRedis(redisConnectionString, name: "redis")
    .AddRabbitMQ(
        sp => connect,
        name: "rabbitmq"
    )
    .AddHangfire(null, name: "hangfire");

builder.Services.AddScoped<CommandDispatcher>();
builder.Services.AddScoped<QueryDispatcher>();
builder.Services.AddScoped<DomainEventPublisher>();
builder.Services.AddValidation();
builder.Services.AddHybridCaching(builder.Configuration);
builder.Services.AddRateLimit();
builder.Services.AddCors(options =>
{
    options.AddPolicy("DevCors", policy =>
    {
        policy.SetIsOriginAllowed(_ => true)
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});
var app = builder.Build();
app.UseCors("DevCors");
app.MapPrometheusScrapingEndpoint(); // default: /metrics

app.MapHubs();

app.UseMiddleware<ErrorHandlerMiddleware>();

#region creating standart user
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();

    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser<int>>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();

    string roleName = "User";
    if (!await roleManager.RoleExistsAsync(roleName))
        await roleManager.CreateAsync(new IdentityRole<int>(roleName));

    string email = "admin@restaurant.com";
    string password = "Admin123!";

    var user = await userManager.FindByEmailAsync(email);

    if (user == null)
    {
        user = new IdentityUser<int>
        {
            UserName = email,
            Email = email,
            EmailConfirmed = true
        };

        var result = await userManager.CreateAsync(user, password);

        if (result.Succeeded)
        {
            await userManager.AddToRoleAsync(user, roleName);
        }
    }
}
#endregion

app.UseHangfireDashboard("/hangfire");
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = async (context, report) =>
    {
        context.Response.ContentType = "application/json";
        var result = System.Text.Json.JsonSerializer.Serialize(new
        {
            status = report.Status.ToString(),
            checks = report.Entries.Select(e => new
            {
                name = e.Key,
                status = e.Value.Status.ToString(),
                exception = e.Value.Exception?.Message,
                duration = e.Value.Duration.ToString()
            })
        });
        await context.Response.WriteAsync(result);
    }
});
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("swagger/v1/swagger.json", "Restaurant System");
    options.RoutePrefix = "";
    options.EnableFilter();
    options.DisplayRequestDuration();
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

ServiceCollectionExtensions.AddJobs();
app.Run();
