using Domain.Events;
using Domain.Events.ReservationCreated;

namespace Application.EventHandlers;

public class ReservationCreatedEventHandler
    : IDomainEventHandler<ReservationCreatedEvent>
{
    public Task HandleAsync(ReservationCreatedEvent domainEvent, CancellationToken ct)
    {
        Console.WriteLine($"Reservation {domainEvent.ReservationId} created!");
        return Task.CompletedTask;
    }
}