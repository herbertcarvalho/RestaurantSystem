namespace Domain.Events;

public record ReservationConfirmedEvent(int RestaurantId, int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => throw new NotImplementedException();
}