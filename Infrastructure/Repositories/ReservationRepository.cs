using Domain.Common;
using Domain.Entities;
using Domain.Extensions;
using Domain.Repositories;
using Domain.Utils.Classes;
using Domain.ValueObjects;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class ReservationRepository(ApplicationDbContext dbContext) : RepositoryAsync<Reservation>(dbContext), IReservationRepository
{
    public async Task<PaginatedResponse<Reservation>> Get(string customerName, EnumReservationStatus? status, DateTime? start, DateTime? end, PageOption pageOption)
    {
        var query = Query()
                    .Include(x => x.Customer)
                    .AsQueryable();

        if (customerName is not null)
            query = query.Where(x => x.Customer.Name.Contains(customerName));

        if (status is not null)
            query = query.Where(x => x.Status == (int)status);

        if (start is not null)
            query = query.Where(x => x.ReservationDate >= start);

        if (end is not null)
            query = query.Where(x => x.ReservationDate <= end);

        return await query
                .ToPaginatedListAsync(pageOption);
    }

    public async Task<Reservation> Get(string reservationCode)
        => await Query().FirstOrDefaultAsync(x => x.Guid.ToString() == reservationCode);

    public async Task<bool> Any(string transactionId)
        => await Query().AnyAsync(x => x.transactionId == transactionId);

    public async Task<ICollection<Reservation>> GetNoShowReservations()
        => await Query()
                    .Where(x => x.Status == (int)EnumReservationStatus.CONFIRMED
                            && x.ReservationDate < DateTime.UtcNow.AddMinutes(-30))
                    .ToListAsync();

    public async Task<ICollection<Reservation>> Get(ICollection<int> ids)
        => await Query()
                    .Where(x => ids.Contains(x.Id))
                    .ToListAsync();
}
