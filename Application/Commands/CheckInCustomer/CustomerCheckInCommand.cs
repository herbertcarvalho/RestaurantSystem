using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.CheckInCustomer;

public record CustomerCheckInCommand(
    DateTime CheckInTime,
    string Notes
    ) : ICommand<ApiResponse<string>>;
