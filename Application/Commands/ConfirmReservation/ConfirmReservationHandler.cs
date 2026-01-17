using Application.Interfaces;
using Domain.Common;
using Domain.Events;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Commands.ConfirmReservation;

public class ConfirmReservationHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    DomainEventPublisher domainEventPublisher
    ) : ICommandWithIdHandler<ConfirmReservationCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> HandleAsync(int id, ConfirmReservationCommand command, CancellationToken ct = default)
    {
        var reservation = await reservationRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("The reservation selected not exists.");

        if (reservation.Status != (int)EnumReservationStatus.PENDING)
            throw new InvalidActionException("The reservation status is not pending.");

        reservation.Status = (int)EnumReservationStatus.CONFIRMED;

        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        await domainEventPublisher.PublishAsync(new ReservationConfirmedEvent(reservation.RestaurantId, reservation.Id));

        return ApiResponse<string>.Success("");
    }
}

public class ConfirmReservationWebhookHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    DomainEventPublisher domainEventPublisher
    ) : ICommandHandler<ConfirmReservationWebhookCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> HandleAsync(ConfirmReservationWebhookCommand command, CancellationToken ct = default)
    {
        if (await reservationRepository.Any(command.TransactionId))
            throw new InvalidActionException("This transaction is already computed.");

        var reservation = await reservationRepository.Get(command.ReservationCode)
            ?? throw new NotFoundException("The reservation selected not exists.");

        if (reservation.Status != (int)EnumReservationStatus.PENDING)
            throw new InvalidActionException("The reservation status is not pending.");

        reservation.Status = (int)EnumReservationStatus.CONFIRMED;
        reservation.DepositAmountPaid = command.Amount;
        reservation.transactionId = command.TransactionId;

        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        await domainEventPublisher.PublishAsync(new ReservationConfirmedEvent(reservation.RestaurantId, reservation.Id));

        return ApiResponse<string>.Success("");
    }
}
