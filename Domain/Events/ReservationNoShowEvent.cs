namespace Domain.Events;

public record ReservationNoShowEvent(int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
