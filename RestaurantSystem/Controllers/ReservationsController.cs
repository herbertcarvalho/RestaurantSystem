
using Application.Commands.CreateReservation;
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
        var command = new CreateReservationCommand { /* map properties */ };
        var result = await _commandDispatcher.DispatchAsync<CreateReservationCommand, Guid>(command);
        return Created($"/api/v1/reservations/{result}", result);
    }

}
