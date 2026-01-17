using Application.Interfaces;
using Domain.Common;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Queries.GetAllReservationsPaged;

public class GetAllReservationsHandler(
    IReservationRepository reservationRepository
    ) : IQueryHandler<GetAllReservationsQuery, PaginatedResponse<GetAllReservationsResponse>>
{
    public async Task<PaginatedResponse<GetAllReservationsResponse>> HandleAsync(GetAllReservationsQuery query, CancellationToken ct = default)
    {
        var response = await reservationRepository.Get(query.CustomerName, query.Status, query.DateStart, query.DateEnd, query);

        var itemsResponse = response.Items.Select(x => new GetAllReservationsResponse()
        {
            CustomerName = x.Customer.Name,
            NumberOfGuests = x.NumberOfGuests,
            ReservationCode = x.Guid.ToString(),
            ReservationDate = x.ReservationDate,
            ReservationId = x.Id,
            Status = ((EnumReservationStatus)x.Status).ToString()
        }).ToList();

        return PaginatedResponse<GetAllReservationsResponse>.Success(itemsResponse, response.TotalCount, response.CurrentPage, response.PageSize);
    }
}
