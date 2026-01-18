using Application.Commands.Login;
using Application.Interfaces;
using Domain.Common;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Services.TokenService;

namespace Application.Commands.RefreshTkn;

public class RefreshTokenHandler(
    IRefreshTokenRepository refreshTokenRepository,
    TokenService tokenService
    ) : ICommandHandler<RefreshTokenCommand, ApiResponse<LoginCommandResponse>>
{
    public async Task<ApiResponse<LoginCommandResponse>> HandleAsync(RefreshTokenCommand command, CancellationToken ct = default)
    {
        var refreshToken = await refreshTokenRepository.Get(command.RefreshToken)
            ?? throw new InvalidActionException("This refresh token is invalid.");

        if (refreshToken.IsUsed)
            throw new InvalidActionException("This refresh token is already used.");

        if (refreshToken.ExpiresIn.CompareTo(DateTime.UtcNow) > 1)
            throw new InvalidActionException("This refresh token is expired.");

        var tokens = await tokenService.GenerateToken();

        LoginCommandResponse response = new LoginCommandResponse(tokens.token, tokens.refresh, 3600);

        return ApiResponse<LoginCommandResponse>.Success(response);
    }
}
