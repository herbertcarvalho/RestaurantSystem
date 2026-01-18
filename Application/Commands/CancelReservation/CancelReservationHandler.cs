using Application.Interfaces;
using Domain.Common;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Commands.CancelReservation;

public class CancelReservationHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork
    ) : ICommandWithIdHandler<CancelReservationCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> HandleAsync(int id, CancelReservationCommand command, CancellationToken ct = default)
    {
        var reservation = await reservationRepository.GetByIdAsync(id)
           ?? throw new NotFoundException("The reservation selected not exists.");

        if (reservation.Status == (int)EnumReservationStatus.COMPLETED
            || reservation.Status == (int)EnumReservationStatus.CANCELLED
            || reservation.Status == (int)EnumReservationStatus.NO_SHOW
            )
            throw new InvalidActionException("The status of reservation not allows to cancel.");

        if (reservation.DepositAmountPaid is null)
            throw new InvalidActionException("The reservation not paid anything.");

        var dateTimeNow = DateTime.UtcNow;
        var refoundClient = reservation.DepositAmountPaid;

        var message = $"Refound 100% = {refoundClient:C}";
        var dateTime = reservation.ReservationDate.Subtract(dateTimeNow).Hours;
        if (dateTime < 24 && dateTime > 2)
            message = $"Refound 50% = {(refoundClient.Value * 0.5M):C}";
        if (dateTime < 2)
            message = "No refound.";

        reservation.Status = (int)EnumReservationStatus.CANCELLED;
        reservation.Notes += $"\n{dateTimeNow} : {command.Reason} + {command.CancelledBy}";

        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success(message);
    }
}
