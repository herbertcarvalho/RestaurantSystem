using Application.Interfaces;
using Domain.Common;
using Domain.Entities;
using Domain.Exceptions;
using Domain.Repositories;
using Domain.ValueObjects;

namespace Application.Commands.ReviewReservation;

public class ReviewReservationHandler(
     IReservationRepository reservationRepository,
     IUnitOfWork unitOfWork,
     IRestaurantReviewRepository restaurantReviewRepository
    ) : ICommandWithIdHandler<ReviewReservationCommand, ApiResponse<string>>
{
    public async Task<ApiResponse<string>> HandleAsync(int id, ReviewReservationCommand command, CancellationToken ct = default)
    {
        var reservation = await reservationRepository.GetByIdAsync(id)
           ?? throw new NotFoundException("The reservation selected not exists.");

        if (reservation.Status != (int)EnumReservationStatus.REVIEW)
            throw new InvalidActionException("The client of reservation not in review.");

        var newReview = new RestaurantReview()
        {
            Category = command.Category,
            Rating = command.Rating,
            Comment = command.Comment,
            RestaurantId = reservation.RestaurantId,
        };

        reservation.Status = (int)EnumReservationStatus.COMPLETED;

        await restaurantReviewRepository.AddAsync(newReview);
        await reservationRepository.UpdateAsync(reservation);
        await unitOfWork.SaveChangesAsync();

        return ApiResponse<string>.Success("");
    }
}
