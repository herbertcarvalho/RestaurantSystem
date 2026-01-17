using Application.Interfaces;
using Domain.Common;
using Domain.Utils.Classes;
using Domain.ValueObjects;

namespace Application.Queries.GetAllReservationsPaged;

public record GetAllReservationsQuery : PageOption, IQuery<PaginatedResponse<GetAllReservationsResponse>>
{
    public string? CustomerName { get; init; }
    public EnumReservationStatus? Status { get; init; }
    public DateTime? DateStart { get; init; }
    public DateTime? DateEnd { get; init; }
}

public record GetAllReservationsResponse : PageOption
{
    public int ReservationId { get; set; }
    public string ReservationCode { get; set; }
    public string CustomerName { get; set; }
    public DateTime ReservationDate { get; set; }
    public int NumberOfGuests { get; set; }
    public string Status { get; set; }
}
