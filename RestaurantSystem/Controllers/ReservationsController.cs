
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
    public async Task<IActionResult> CreateReservation(/*[FromBody] CreateReservationRequest request*/)
    {
        var command = new CreateReservationCommand("", "", "", DateTime.Now, 4, 4, null, null, null);
        var result = await _commandDispatcher.DispatchAsync<CreateReservationCommand, ApiResponse<Guid>>(command);
        return Created($"/api/v1/reservations/{result}", result);
    }

}
