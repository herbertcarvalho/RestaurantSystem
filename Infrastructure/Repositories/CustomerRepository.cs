using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class CustomerRepository(ApplicationDbContext dbContext) : RepositoryAsync<Customer>(dbContext), ICustomerRepository
{
    public async Task<Customer> Get(string email)
        => await Query().FirstOrDefaultAsync(x => x.Email == email);
}
