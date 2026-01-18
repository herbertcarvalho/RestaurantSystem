using Application.Extensions;
using Application.Interfaces;
using Domain.Events;
using Hangfire;
using Infrastructure.DbContext;
using Infrastructure.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Api.Middleware;
using ServiceCollectionExtensions = Infrastructure.Extensions.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

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
builder.Services.AddHealthChecks().AddDbContextCheck<ApplicationDbContext>();
builder.Services.AddScoped<CommandDispatcher>();
builder.Services.AddScoped<QueryDispatcher>();
builder.Services.AddScoped<DomainEventPublisher>();
builder.Services.AddValidation();
builder.Services.AddHangfireServiceSignalR(builder.Configuration);
builder.Services.AddHybridCaching(builder.Configuration);
builder.Services.AddRateLimit();
builder.Services.AddVersioning();
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
