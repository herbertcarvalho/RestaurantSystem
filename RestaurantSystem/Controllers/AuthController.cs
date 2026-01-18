using Application.Commands.Login;
using Application.Commands.RefreshTkn;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class AuthController(CommandDispatcher commandDispatcher) : ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> LoginCommand([FromBody] LoginCommand loginCommand)
    {
        var result = await commandDispatcher.DispatchAsync<LoginCommand, ApiResponse<LoginCommandResponse>>(loginCommand);
        return Ok(result);
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> RefreshTokenCommand([FromBody] RefreshTokenCommand refreshTokenCommand)
    {
        var result = await commandDispatcher.DispatchAsync<RefreshTokenCommand, ApiResponse<LoginCommandResponse>>(refreshTokenCommand);
        return Ok(result);
    }
}
