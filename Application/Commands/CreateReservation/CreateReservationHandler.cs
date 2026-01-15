using Application.Interfaces;

namespace Application.Commands.CreateReservation;

public class CreateReservationHandler() : ICommandHandler<CreateReservationCommand, Guid>
{

    public async Task<Guid> HandleAsync(CreateReservationCommand command, CancellationToken ct = default)
    {
        throw new NotImplementedException();
    }
}
