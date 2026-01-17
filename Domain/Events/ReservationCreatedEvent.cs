namespace Domain.Events;

public record ReservationCreatedEvent(int RestaurantId, int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
