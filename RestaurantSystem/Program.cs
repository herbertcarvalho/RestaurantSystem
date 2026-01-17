using Application.Extensions;
using Application.Interfaces;
using Domain.Events;
using Hangfire;
using Infrastructure.DbContext;
using Infrastructure.Extensions;
using Microsoft.EntityFrameworkCore;
using RestaurantSystem.Api.Middleware;
using ServiceCollectionExtensions = Infrastructure.Extensions.ServiceCollectionExtensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
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

app.MapHubs();

app.UseMiddleware<ErrorHandlerMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}
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
