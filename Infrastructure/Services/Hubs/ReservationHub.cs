using Microsoft.AspNetCore.SignalR;

namespace Infrastructure.Services.Hubs;

public class ReservationHub : Hub
{
    private Dictionary<int, string> dicMap = new()
    {
        { 1, "CustomerCheckIn" },
        { 2, "ReservationConfirmed" },
        { 3, "ReservationCreated" },
        { 4, "ReservationNoShow" }
    };

    public async Task SubscribeToRestaurant(int restaurantId)
    {
        var groupName = $"restaurant-{restaurantId}";

        await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
    }

    public async Task SendMessage(int restaurantId, int reservationId, int type, string message)
    {
        var groupName = $"restaurant-{restaurantId}";

        await Clients.Group(groupName)
            .SendAsync(dicMap[type], new
            {
                ReservationId = reservationId,
                Message = "New reservation created!",
                At = DateTime.UtcNow
            });
    }
}
