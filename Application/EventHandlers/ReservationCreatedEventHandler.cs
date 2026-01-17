using Domain.Events;
using Infrastructure.Services.Notifier;

namespace Application.EventHandlers;

public class ReservationCreatedEventHandler(
    ReservationNotifier reservationNotifier
    )
    : IDomainEventHandler<ReservationCreatedEvent>
{
    public async Task HandleAsync(ReservationCreatedEvent domainEvent, CancellationToken ct)
    {
        await reservationNotifier.SendMessage(domainEvent.RestaurantId, domainEvent.ReservationId, 3, $"Reservation {domainEvent.ReservationId} created!");

        Console.WriteLine($"Reservation {domainEvent.ReservationId} created!");
    }
}