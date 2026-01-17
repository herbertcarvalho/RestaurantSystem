using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class RestaurantRepository(ApplicationDbContext dbContext) : RepositoryAsync<Restaurant>(dbContext), IRestaurantRepository
{
    public Task<bool> Any(int id)
        => Query().AnyAsync(x => x.Id == id);
}
