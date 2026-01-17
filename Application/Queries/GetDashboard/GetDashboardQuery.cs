using Application.Interfaces;
using Domain.Common;

namespace Application.Queries.GetDashboard;

public record GetDashboardQuery(int restaurantId) : IQuery<ApiResponse<GetDashboardResponse>>
{
}

public record GetDashboardResponse(
    GetDashboardTodayReservations TodayReservations,
    decimal MonthlyNoShowRate,
    List<GetDashboardUpcomingReservation> UpcomingReservations
);

public record GetDashboardTodayReservations(
    int Total,
    int Confirmed,
    int Pending,
    int CheckedIn
);

public record GetDashboardUpcomingReservation(
    string CustomerName,
    DateTime ReservationTime,
    int NumberOfGuests,
    int MinutesUntilArrival
);
