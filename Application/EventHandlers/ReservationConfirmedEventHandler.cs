using Domain.Events;
using Domain.Events.ReservationConfirmed;

namespace Application.EventHandlers;

public class ReservationConfirmedEventHandler
    : IDomainEventHandler<ReservationConfirmedEvent>
{
    public Task HandleAsync(ReservationConfirmedEvent domainEvent, CancellationToken ct)
    {
        Console.WriteLine($"Reservation {domainEvent.ReservationId} confirmed!");
        return Task.CompletedTask;
    }
}