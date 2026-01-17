namespace Domain.Events.ReservationConfirmed;

public record ReservationConfirmedEvent(int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => throw new NotImplementedException();
}