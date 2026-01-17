using Domain.Entities;

namespace Domain.Repositories;

public interface ICustomerRepository : IRepositoryAsync<Customer>
{
    Task<Customer> Get(string email);
}