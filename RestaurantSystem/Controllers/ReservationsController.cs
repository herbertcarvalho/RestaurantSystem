
using Application.Commands.CreateReservation;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController : ControllerBase
{
    private readonly CommandDispatcher commandDispatcher;

    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    ];

    public ReservationsController(CommandDispatcher _commandDispatcher)
    {
        commandDispatcher = _commandDispatcher;
    }

    [HttpPost]
    public async Task<IActionResult> CreateReservation(/*[FromBody] CreateReservationRequest request*/)
    {
        var command = new CreateReservationCommand { /* map properties */ };
        var result = await commandDispatcher.DispatchAsync<CreateReservationCommand, Guid>(command);
        return Created($"/api/v1/reservations/{result}", result);
    }

}
