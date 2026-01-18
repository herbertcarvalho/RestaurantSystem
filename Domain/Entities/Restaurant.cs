using Domain.Extensions;

namespace Domain.Entities;

public class Restaurant : Entity
{
    public required string Name { get; set; }

    public virtual ICollection<Reservation> Reservations { get; set; }
    public virtual ICollection<RestaurantReview> Reviews { get; set; }
}
