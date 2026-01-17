using Application.Interfaces;
using Domain.Common;
using Domain.Repositories;
using Domain.Utils.Classes;
using Domain.ValueObjects;
using Microsoft.Extensions.Caching.Hybrid;

namespace Application.Queries.GetDashboard;

public class GetDashboardHandler(IReservationRepository reservationRepository,
    HybridCache hybridCache) : IQueryHandler<GetDashboardQuery, ApiResponse<GetDashboardResponse>>
{
    public async Task<ApiResponse<GetDashboardResponse>> HandleAsync(GetDashboardQuery query, CancellationToken ct = default)
    {
        var cacheKey = $"dashboard:metrics:restaurant-{query.restaurantId}";

        return await hybridCache.GetOrCreateAsync(
            cacheKey,
            async token => await CalculateMetrics(query)
        );
    }

    private async Task<ApiResponse<GetDashboardResponse>> CalculateMetrics(GetDashboardQuery query)
    {
        var now = DateTime.UtcNow;

        var startOfMonth = new DateTime(now.Year, now.Month, 1, 0, 0, 0, DateTimeKind.Utc);
        var endOfMonth = new DateTime(now.Year, now.Month + 1, 1, 0, 0, 0, DateTimeKind.Utc).AddTicks(-1);

        var startOfDay = new DateTime(now.Year, now.Month, now.Day, 0, 0, 0, DateTimeKind.Utc);
        var endOfDay = new DateTime(now.Year, now.Month, now.Day, 23, 59, 59, DateTimeKind.Utc);

        var reservations = (await reservationRepository.Get(null, null, startOfMonth, endOfMonth, new PageOption() { PageSize = 20000 }, restaurantId: query.restaurantId)).Items;

        var reservationsToday = reservations.Where(x => x.ReservationDate <= endOfDay && x.ReservationDate >= startOfDay).ToList();

        var responseDashToday = new GetDashboardTodayReservations(
            reservationsToday.Count,
            reservationsToday.Count(x => x.Status == (int)EnumReservationStatus.CONFIRMED),
            reservationsToday.Count(x => x.Status == (int)EnumReservationStatus.PENDING),
            reservationsToday.Count(x => x.Status == (int)EnumReservationStatus.CHECKED_IN)
            );

        var monthNoShowRate = reservations.Count(x => x.Status == (int)EnumReservationStatus.NO_SHOW) * 100 / reservations.Count;

        List<GetDashboardUpcomingReservation> upcomingReservations = [.. reservations
            .Where(x => x.ReservationDate >= now && x.ReservationDate <= now.AddHours(2))
            .Select(x => new GetDashboardUpcomingReservation(x.Customer.Name, x.ReservationDate, x.NumberOfGuests, x.ReservationDate.Subtract(now).Minutes))];

        var response = new GetDashboardResponse(responseDashToday, monthNoShowRate, upcomingReservations);

        return ApiResponse<GetDashboardResponse>.Success(response);
    }
}
