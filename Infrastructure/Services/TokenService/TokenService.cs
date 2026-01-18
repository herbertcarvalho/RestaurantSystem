using Domain.Entities;
using Domain.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Infrastructure.Services.TokenService;

public class TokenService(
    IConfiguration configuration,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork)
{
    public async Task<(string token, string refresh)> GenerateToken()
    {
        // i know this code is reap
        var expiration = DateTime.Now.AddHours(1);
        var key = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(configuration["Jwt:key"]));
        var credenciais = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
        JwtSecurityToken token = new(
              issuer: configuration["TokenConfiguration:Issuer"],
              audience: configuration["TokenConfiguration:Audience"],
              claims: [],
              expires: expiration,
              signingCredentials: credenciais);

        var newRefreshToken = new RefreshToken()
        {
            Token = Guid.NewGuid().ToString(),
            ExpiresIn = DateTime.UtcNow.AddDays(7),
            IsUsed = false
        };
        await refreshTokenRepository.AddAsync(newRefreshToken);
        await unitOfWork.SaveChangesAsync();

        return new(new JwtSecurityTokenHandler().WriteToken(token), newRefreshToken.Token);
    }
}
