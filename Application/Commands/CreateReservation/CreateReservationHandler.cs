using Application.Common;
using Application.Interfaces;

namespace Application.Commands.CreateReservation;

public class CreateReservationHandler() : ICommandHandler<CreateReservationCommand, ApiResponse<Guid>>
{
    public async Task<ApiResponse<Guid>> HandleAsync(CreateReservationCommand command, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
