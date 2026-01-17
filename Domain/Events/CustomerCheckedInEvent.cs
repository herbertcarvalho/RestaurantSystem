namespace Domain.Events;

public record CustomerCheckedInEvent(int RestaurantId, int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
