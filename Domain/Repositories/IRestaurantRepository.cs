using Domain.Entities;

namespace Domain.Repositories;

public interface IRestaurantRepository : IRepositoryAsync<Restaurant>
{
    Task<bool> Any(int id);
}
