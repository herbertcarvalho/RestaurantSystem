using Application.Commands.Login;
using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.RefreshTkn;

public record RefreshTokenCommand(string RefreshToken) : ICommand<ApiResponse<LoginCommandResponse>>
{
}