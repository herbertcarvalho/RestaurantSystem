using Application.Interfaces;
using Domain.Common;

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
    ) : ICommand<ApiResponse<CreateReservationCommandResponse>>;

public record CreateReservationCommandResponse(
    string ReservationCode,
    int ReservationId,
    string Status
    );
