namespace Domain.Events;

public record ReservationNoShowEvent(int RestaurantId, int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
