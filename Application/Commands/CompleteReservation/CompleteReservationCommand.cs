using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.CompleteReservation;

public record CompleteReservationCommand() : ICommand<ApiResponse<string>>;
