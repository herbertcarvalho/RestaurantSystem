
using Domain.Events;
using Domain.Repositories;
using Domain.ValueObjects;
using Infrastructure.RabbitMQ.Publishers;

namespace Infrastructure.Services.ReservationServ;

public class ReservationService(
    IReservationRepository reservationRepository,
    IUnitOfWork unitOfWork,
    IRabbitMqPublisher rabbitMqPublisher,
    DomainEventPublisher domainEventPublisher
    ) : IReservationService
{
    public async Task ProcessNoShowsAsync(ICollection<int> reservationIds)
    {
        var reservations = await reservationRepository.Get(reservationIds);

        foreach (var reservation in reservations)
        {
            reservation.Status = (int)EnumReservationStatus.NO_SHOW;
            await domainEventPublisher.PublishAsync(new ReservationNoShowEvent(reservation.RestaurantId, reservation.Id));
        }

        await reservationRepository.UpdateRangeAsync(reservations);
        await unitOfWork.SaveChangesAsync();
    }

    public async Task ProcessNoShowsAsync()
    {
        var reservations = await reservationRepository.GetNoShowReservations();

        if (reservations.Count == 0)
            return;

        if (reservations.Count <= 1000)
        {
            foreach (var reservation in reservations)
            {
                reservation.Status = (int)EnumReservationStatus.NO_SHOW;
                await domainEventPublisher.PublishAsync(new ReservationNoShowEvent(reservation.RestaurantId, reservation.Id));
            }

            await reservationRepository.UpdateRangeAsync([.. reservations]);
            await unitOfWork.SaveChangesAsync();
        }
        else
            foreach (var item in reservations.Select(x => x.Id).Chunk(100).ToList())
                await rabbitMqPublisher.Publish("no-show", item);

    }
}
