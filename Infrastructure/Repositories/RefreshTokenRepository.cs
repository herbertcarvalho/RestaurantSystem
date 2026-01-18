using Domain.Entities;
using Domain.Repositories;
using Infrastructure.DbContext;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

internal class RefreshTokenRepository(ApplicationDbContext dbContext) : RepositoryAsync<RefreshToken>(dbContext), IRefreshTokenRepository
{
    public async Task<RefreshToken> Get(string token)
        => await Query().FirstOrDefaultAsync(x => x.Token == token);
    public async Task<ICollection<RefreshToken>> GetTokensUnsed()
        => await Query().Where(x => !x.IsUsed).ToListAsync();
}
