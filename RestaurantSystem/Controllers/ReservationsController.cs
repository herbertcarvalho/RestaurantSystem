
using Application.Commands.CreateReservation;
using Application.Common;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController(CommandDispatcher _commandDispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand request)
    {
        var result = await _commandDispatcher.DispatchAsync<CreateReservationCommand, ApiResponse<Guid>>(request);
        return Created($"/api/v1/reservations/{result}", result);
    }
}
