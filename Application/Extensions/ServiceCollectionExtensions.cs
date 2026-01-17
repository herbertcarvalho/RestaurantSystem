using Application.Commands.CheckInCustomer;
using Application.Commands.ConfirmReservation;
using Application.Commands.CreateReservation;
using Application.EventHandlers;
using Application.Interfaces;
using Application.Queries.GetAllReservationsPaged;
using Domain.Common;
using Domain.Events;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions;

public static class ServiceCollectionExtensions
{
    public static void AddCommands(this IServiceCollection services)
    {
        services.AddScoped<ICommandHandler<CreateReservationCommand, ApiResponse<CreateReservationCommandResponse>>, CreateReservationHandler>();

        services.AddScoped<ICommandWithIdHandler<ConfirmReservationCommand, ApiResponse<string>>, ConfirmReservationHandler>();

        services.AddScoped<ICommandHandler<ConfirmReservationWebhookCommand, ApiResponse<string>>, ConfirmReservationWebhookHandler>();

        services.AddScoped<ICommandWithIdHandler<CustomerCheckInCommand, ApiResponse<string>>, CustomerCheckInHandler>();
    }

    public static void AddQueries(this IServiceCollection services)
    {
        services.AddScoped<IQueryHandler<GetAllReservationsQuery, PaginatedResponse<GetAllReservationsResponse>>, GetAllReservationsHandler>();
    }

    public static void AddDomainEvents(this IServiceCollection services)
    {
        services.AddScoped<IDomainEventHandler<ReservationConfirmedEvent>, ReservationConfirmedEventHandler>();
        services.AddScoped<IDomainEventHandler<ReservationCreatedEvent>, ReservationCreatedEventHandler>();
        services.AddScoped<IDomainEventHandler<CustomerCheckedInEvent>, CustomerCheckedInEventHandler>();
        services.AddScoped<IDomainEventHandler<ReservationNoShowEvent>, ReservationNoShowEventHandler>();
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
