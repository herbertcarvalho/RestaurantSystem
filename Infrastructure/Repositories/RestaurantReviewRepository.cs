using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DbContext;

namespace Infrastructure.Repositories;

public class RestaurantReviewRepository(ApplicationDbContext dbContext) : RepositoryAsync<RestaurantReview>(dbContext), IRestaurantReviewRepository
{
}
