using Domain.Events;

namespace Application.EventHandlers;

public class ReservationNoShowEventHandler
    : IDomainEventHandler<ReservationNoShowEvent>
{
    public Task HandleAsync(ReservationNoShowEvent domainEvent, CancellationToken ct)
    {
        Console.WriteLine($"Reservation {domainEvent.ReservationId} no-show!");
        return Task.CompletedTask;
    }
}