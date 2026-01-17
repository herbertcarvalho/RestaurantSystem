using Domain.Repositories;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore.Storage;

namespace Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext dbContext) : IUnitOfWork
{
    private bool disposed;
    private IDbContextTransaction transaction;

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return dbContext.SaveChangesAsync(cancellationToken);
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!disposed && disposing)
        {
            dbContext.Dispose();
        }
        disposed = true;
    }

    public async Task<IDbContextTransaction> BeginTransactionAsync()
        => await dbContext.Database.BeginTransactionAsync();

}