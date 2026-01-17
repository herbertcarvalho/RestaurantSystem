using Domain.Extensions;

namespace Domain.Entities;

public class Customer : Entity
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; }
}
