namespace Domain.Events;

public record CustomerCheckedInEvent(int ReservationId) : IDomainEvent
{
    public DateTime OccurredOn => DateTime.UtcNow;
}
