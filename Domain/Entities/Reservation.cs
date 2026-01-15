using Domain.Extensions;

namespace Domain.Entities;

public class Reservation : Entity
{
    public int Status { get; set; }
    public int CustomerId { get; set; }
    public int RestaurantId { get; set; }
    public int NumberOfGuests { get; set; }
    public Guid Guid { get; set; }
    public bool? RequiresDeposit { get; set; }
    public string? SpecialRequests { get; set; }
    public decimal? DepositAmount { get; set; }

    public virtual Customer Customer { get; set; }
    public virtual Restaurant Restaurant { get; set; }
}
