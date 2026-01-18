using Application.Interfaces;
using Domain.Common;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Commands.CompleteReservation;

public class CompleteReservationHandler(
     IReservationRepository reservationRepository,
     IUnitOfWork unitOfWork
    ) : ICommandWithIdHandler<CompleteReservationCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> HandleAsync(int id, CompleteReservationCommand command, CancellationToken ct = default)
    {
        var reservation = await reservationRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("The reservation selected not exists.");

        if (reservation.Status != (int)EnumReservationStatus.CHECKED_IN)
            throw new InvalidActionException("The client of reservation not made the check-in.");

        reservation.Status = (int)EnumReservationStatus.REVIEW;

        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success("");
    }
}
