using Domain.Entities;

namespace Domain.Repositories;

public interface IRefreshTokenRepository : IRepositoryAsync<RefreshToken>
{
    Task<RefreshToken> Get(string token);
    Task<ICollection<RefreshToken>> GetTokensUnsed();
}