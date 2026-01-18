using Domain.Extensions;

namespace Domain.Entities;

public class RestaurantReview : Entity
{
    public int Rating { get; set; }
    public int RestaurantId { get; set; }
    public string Comment { get; set; }
    public string Category { get; set; }

    public virtual Restaurant Restaurant { get; set; }
}
