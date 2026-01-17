using Domain.Common;
using Domain.Entities;
using Domain.Utils.Classes;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface IReservationRepository : IRepositoryAsync<Reservation>
{
    Task<PaginatedResponse<Reservation>> Get(string customerName, EnumReservationStatus? status, DateTime? start, DateTime? end, PageOption pageOption);
}
