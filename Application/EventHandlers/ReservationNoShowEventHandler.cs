using Domain.Events;
using Infrastructure.Services.Notifier;

namespace Application.EventHandlers;

public class ReservationNoShowEventHandler(
    ReservationNotifier reservationNotifier
    )
    : IDomainEventHandler<ReservationNoShowEvent>
{
    public async Task HandleAsync(ReservationNoShowEvent domainEvent, CancellationToken ct)
    {
        await reservationNotifier.SendMessage(domainEvent.RestaurantId, domainEvent.ReservationId, 4, $"Reservation {domainEvent.ReservationId} no-show!");

        Console.WriteLine($"Reservation {domainEvent.ReservationId} no-show!");
    }
}