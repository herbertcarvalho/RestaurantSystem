using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.CancelReservation;

public record CancelReservationCommand(
    string Reason,
    string CancelledBy
    ) : ICommand<ApiResponse<string>>;
