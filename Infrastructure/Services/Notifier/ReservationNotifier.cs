using Infrastructure.Services.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services.Notifier;

public class ReservationNotifier
{
    private readonly IHubContext<ReservationHub> _hubContext;

    public ReservationNotifier(IHubContext<ReservationHub> hubContext)
    {
        _hubContext = hubContext;
    }

    private Dictionary<int, string> dicMap = new()
    {
        { 1, "CustomerCheckIn" },
        { 2, "ReservationConfirmed" },
        { 3, "ReservationCreated" },
        { 4, "ReservationNoShow" }
    };

    public async Task SendMessage(int restaurantId, int reservationId, int type, string message)
    {
        var groupName = $"restaurant-{restaurantId}";
        var eventName = dicMap[type];

        await _hubContext.Clients
            .Group(groupName)
            .SendAsync(eventName, new
            {
                ReservationId = reservationId,
                Message = message,
                At = DateTime.UtcNow
            });
    }
}
