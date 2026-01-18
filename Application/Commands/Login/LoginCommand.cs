using Application.Interfaces;
using Domain.Common;

namespace Application.Commands.Login;

public record LoginCommand(string Email, string Password) : ICommand<ApiResponse<LoginCommandResponse>>;

public record LoginCommandResponse(string AccessToken, string RefreshToken, int ExpiresIn);