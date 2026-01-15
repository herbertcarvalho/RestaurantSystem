using Domain.Extensions;

namespace Domain.Entities;

public class ReservationStatus : Entity
{
    public required string Name { get; set; }
}
