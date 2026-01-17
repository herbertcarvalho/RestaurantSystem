using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Commands.CreateReservation;

public class CreateReservationHandler(
    IReservationRepository reservationRepository,
    IRestaurantRepository restaurantRepository,
    ICustomerRepository customerRepository,
    IUnitOfWork unitOfWork
    ) : ICommandHandler<CreateReservationCommand, ApiResponse<CreateReservationCommandResponse>>
{
    public async Task<ApiResponse<CreateReservationCommandResponse>> HandleAsync(CreateReservationCommand command, CancellationToken ct = default)
    {
        if (!await restaurantRepository.Any(command.RestaurantId))
            throw new NotFoundException("The selected restaurant not exists.");

        var customer = await customerRepository.Get(command.CustomerEmail);
        if (customer is null)
        {
            customer = new Customer()
            {
                Name = command.CustomerName,
                Phone = command.CustomerPhone,
                Email = command.CustomerEmail,
            };

            await customerRepository.AddAsync(customer);
        }

        var newReservation = new Reservation()
        {
            Status = (int)EnumReservationStatus.PENDING,
            RestaurantId = command.RestaurantId,
            NumberOfGuests = command.NumberOfGuests,
            Guid = Guid.NewGuid(),
            RequiresDeposit = command.RequiresDeposit,
            SpecialRequests = command.SpecialRequests,
            DepositAmount = command.DepositAmount,
        };

        var transaction = await unitOfWork.BeginTransactionAsync();

        try
        {
            if (customer.Id <= 0)
                await unitOfWork.SaveChangesAsync();

            newReservation.CustomerId = customer.Id;
            await reservationRepository.AddAsync(newReservation);
            await unitOfWork.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }

        var response = new CreateReservationCommandResponse(newReservation.Guid.ToString(),
            newReservation.Id,
            EnumReservationStatus.PENDING.ToString());

        return ApiResponse<CreateReservationCommandResponse>.Success(response);
    }
}
