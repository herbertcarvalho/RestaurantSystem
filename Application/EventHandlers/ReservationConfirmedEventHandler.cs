using Domain.Events;
using Infrastructure.Services.Notifier;

namespace Application.EventHandlers;

public class ReservationConfirmedEventHandler(
    ReservationNotifier reservationNotifier
    )
    : IDomainEventHandler<ReservationConfirmedEvent>
{
    public async Task HandleAsync(ReservationConfirmedEvent domainEvent, CancellationToken ct)
    {
        await reservationNotifier.SendMessage(domainEvent.RestaurantId, domainEvent.ReservationId, 2, $"Reservation {domainEvent.ReservationId} confirmed!");
        Console.WriteLine($"Reservation {domainEvent.ReservationId} confirmed!");
    }
}