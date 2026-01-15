using Application.Interfaces;

namespace Application.Commands.CreateReservation;

public record CreateReservationCommand(
    string CustomerName,
    string CustomerEmail,
    string CustomerPhone,
    DateTime ReservationDate,
    int NumberOfGuests,
    int RestaurantId,

    string? SpecialRequests,
    bool? RequiresDeposit,
    decimal? DepositAmount
    ) : ICommand<Guid>;
