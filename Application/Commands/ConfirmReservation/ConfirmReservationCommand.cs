using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.ConfirmReservation;

public record ConfirmReservationCommand(
    string ConfirmedBy,
    string Notes
) : ICommand<ApiResponse<string>>;

public record ConfirmReservationWebhookCommand(
    string TransactionId,
    string ReservationCode,
    decimal Amount,
    DateTime PaidAt,
    string Status
) : ICommand<ApiResponse<string>>;

