namespace Domain.Events;

public record ReservationConfirmedEvent(int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => throw new NotImplementedException();
}