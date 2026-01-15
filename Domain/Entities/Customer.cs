using Domain.Extensions;

namespace Domain.Entities;

public class Customer : Entity
{
    public required string Name { get; set; }
    public required string Email { get; set; }
    public required string Phone { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; }
}
