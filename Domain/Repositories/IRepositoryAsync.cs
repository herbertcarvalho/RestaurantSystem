namespace Domain.Repositories;

public interface IRepositoryAsync<T> where T : class
{
    ValueTask<T> GetByIdAsync(int id);

    Task<IEnumerable<T>> GetAllAsync();

    IQueryable<T> Query();

    Task<T> AddAsync(T entity);

    Task UpdateAsync(T entity);

    Task DeleteAsync(T entity);

    Task UpdateRangeAsync(ICollection<T> entity);
}