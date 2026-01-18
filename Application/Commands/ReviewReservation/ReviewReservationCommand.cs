using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.ReviewReservation;

public record ReviewReservationCommand(
    int Rating,
    string Comment,
    string Category
    ) : ICommand<ApiResponse<string>>
{
}
