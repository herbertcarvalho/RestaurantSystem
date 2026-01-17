namespace Infrastructure.Services.ReservationServ;

public interface IReservationService
{
    Task ProcessNoShowsAsync();
    Task ProcessNoShowsAsync(ICollection<int> reservationIds);
}
