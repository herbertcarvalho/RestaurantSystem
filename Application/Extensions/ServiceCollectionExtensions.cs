using Application.Commands.CreateReservation;
using Application.Interfaces;
using Application.Queries.GetAllReservationsPaged;
using Domain.Common;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommands(this IServiceCollection Services)
    {
        Services.AddScoped<ICommandHandler<CreateReservationCommand, ApiResponse<CreateReservationCommandResponse>>, CreateReservationHandler>();
    }

    public static void AddQueries(this IServiceCollection Services)
    {
        Services.AddScoped<IQueryHandler<GetAllReservationsQuery, PaginatedResponse<GetAllReservationsResponse>>, GetAllReservationsHandler>();
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
