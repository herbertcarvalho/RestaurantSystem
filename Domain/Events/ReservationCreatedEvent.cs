namespace Domain.Events;

public record ReservationCreatedEvent(int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
