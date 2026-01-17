using Domain.Events;
using Infrastructure.Services.Notifier;

namespace Application.EventHandlers;

public class CustomerCheckedInEventHandler(
    ReservationNotifier reservationNotifier
    )
    : IDomainEventHandler<CustomerCheckedInEvent>
{
    public async Task HandleAsync(CustomerCheckedInEvent domainEvent, CancellationToken ct)
    {
        await reservationNotifier.SendMessage(domainEvent.RestaurantId, domainEvent.ReservationId, 1, $"Reservation {domainEvent.ReservationId} check-in!");

        Console.WriteLine($"Reservation {domainEvent.ReservationId} check-in!");
    }
}