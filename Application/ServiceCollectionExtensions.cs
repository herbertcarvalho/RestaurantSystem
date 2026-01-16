using Application.Commands.CreateReservation;
using Application.Common;
using Application.Interfaces;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ServiceCollectionExtensions
{
    public static void AddCommands(this IServiceCollection Services)
    {
        Services.AddScoped<ICommandHandler<CreateReservationCommand, ApiResponse<Guid>>, CreateReservationHandler>();
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
