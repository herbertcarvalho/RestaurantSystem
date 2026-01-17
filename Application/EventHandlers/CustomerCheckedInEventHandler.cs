using Domain.Events;

namespace Application.EventHandlers;

public class CustomerCheckedInEventHandler
    : IDomainEventHandler<CustomerCheckedInEvent>
{
    public Task HandleAsync(CustomerCheckedInEvent domainEvent, CancellationToken ct)
    {
        Console.WriteLine($"Reservation {domainEvent.ReservationId} check-in!");
        return Task.CompletedTask;
    }
}