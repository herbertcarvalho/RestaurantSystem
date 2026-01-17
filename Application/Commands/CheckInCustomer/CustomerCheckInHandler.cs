using Application.Interfaces;
using Domain.Common;
using Domain.Events;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Commands.CheckInCustomer;

public class CustomerCheckInHandler(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    DomainEventPublisher domainEventPublisher) : ICommandWithIdHandler<CustomerCheckInCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> HandleAsync(int id, CustomerCheckInCommand command, CancellationToken ct = default)
    {
        var reservation = await reservationRepository.GetByIdAsync(id)
            ?? throw new NotFoundException("The reservation selected not exists.");

        if (reservation.Status != (int)EnumReservationStatus.CONFIRMED)
            throw new InvalidActionException("The reservation status is not confirmed.");

        reservation.Status = (int)EnumReservationStatus.CHECKED_IN;
        reservation.CheckInTime = command.CheckInTime;
        reservation.Notes = reservation.Notes + "\n" + command.Notes;

        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        await domainEventPublisher.PublishAsync(new CustomerCheckedInEvent(reservation.Id));

        return ApiResponse<string>.Success("");
    }
}
