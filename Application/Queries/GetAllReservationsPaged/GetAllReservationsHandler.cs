using Application.Interfaces;
using Domain.Common;
using Domain.Repositories;

namespace Application.Queries.GetAllReservationsPaged;

public class GetAllReservationsHandler(
    IReservationRepository reservationRepository
    ) : IQueryHandler<GetAllReservationsQuery, PaginatedResponse<GetAllReservationsResponse>>
{
    public Task<PaginatedResponse<GetAllReservationsResponse>> HandleAsync(GetAllReservationsQuery query, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
