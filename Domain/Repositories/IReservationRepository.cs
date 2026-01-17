using Domain.Common;
using Domain.Entities;
using Domain.Utils.Classes;
using Domain.ValueObjects;

namespace Domain.Repositories;

public interface IReservationRepository : IRepositoryAsync<Reservation>
{
    Task<PaginatedResponse<Reservation>> Get(string customerName, EnumReservationStatus? status, DateTime? start, DateTime? end, PageOption pageOption, int? restaurantId = null);
    Task<Reservation> Get(string reservationCode);
    Task<bool> Any(string transactionId);

    Task<ICollection<Reservation>> GetNoShowReservations();
    Task<ICollection<Reservation>> Get(ICollection<int> ids);
}
