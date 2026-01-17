using Domain.Repositories;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RepositoryAsync<T>(ApplicationDbContext dbContext) : IRepositoryAsync<T> where T : class
{

    public IQueryable<T> Query() => dbContext.Set<T>().AsNoTracking().AsQueryable();

    protected virtual IQueryable<T> TrackedQuery() => dbContext.Set<T>().AsQueryable();

    public virtual async Task<T> AddAsync(T entity)
    {
        await dbContext.Set<T>().AddAsync(entity);
        return entity;
    }

    public virtual Task DeleteAsync(T entity)
    {
        dbContext.Set<T>().Remove(entity);
        return Task.CompletedTask;
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await dbContext
            .Set<T>()
            .ToListAsync();
    }

    public virtual ValueTask<T> GetByIdAsync(int id) => dbContext.Set<T>().FindAsync(id);

    public virtual Task UpdateAsync(T entity)
    {
        dbContext.Set<T>().Update(entity);
        return Task.CompletedTask;
    }

    public virtual Task UpdateRangeAsync(ICollection<T> entity)
    {
        dbContext.Set<T>().UpdateRange(entity);
        return Task.CompletedTask;
    }
}