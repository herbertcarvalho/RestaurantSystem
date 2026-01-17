namespace Domain.Events.ReservationCreated;

public record ReservationCreatedEvent(int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
