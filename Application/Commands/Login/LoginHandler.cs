using Application.Interfaces;
using Domain.Common;
using Domain.Exceptions;
using Domain.Repositories;
using Infrastructure.Services.TokenService;
using Microsoft.AspNetCore.Identity;

namespace Application.Commands.Login;

public class LoginHandler(
    UserManager<IdentityUser<int>> userManager,
    SignInManager<IdentityUser<int>> signInManager,
    IRefreshTokenRepository refreshTokenRepository,
    IUnitOfWork unitOfWork,
    TokenService tokenService)
    : ICommandHandler<LoginCommand, ApiResponse<LoginCommandResponse>>
{
    public async Task<ApiResponse<LoginCommandResponse>> HandleAsync(LoginCommand command, CancellationToken ct = default)
    {
        var result = await signInManager.PasswordSignInAsync(command.Email,
                command.Password, isPersistent: false, lockoutOnFailure: true);

        if (!result.Succeeded)
        {
            if (result.IsLockedOut)
                throw new InvalidActionException($"Login Bloqueado por excesso de tentativas.");

            if (result.IsNotAllowed)
                throw new InvalidActionException($"Login Bloqueado.");

            throw new InvalidActionException($"Credenciais Inválidas.");
        }

        var identityUser = await userManager.FindByEmailAsync(command.Email);


        var refreshNotUsed = await refreshTokenRepository.GetTokensUnsed();
        foreach (var refresh in refreshNotUsed)
            refresh.IsUsed = true;

        await refreshTokenRepository.UpdateRangeAsync(refreshNotUsed);
        await unitOfWork.SaveChangesAsync();

        var tokens = await tokenService.GenerateToken();

        LoginCommandResponse response = new LoginCommandResponse(tokens.token, tokens.refresh, 3600);

        return ApiResponse<LoginCommandResponse>.Success(response);
    }
}
