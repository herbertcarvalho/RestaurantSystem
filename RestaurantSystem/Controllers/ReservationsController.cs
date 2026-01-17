
using Application.Commands.CreateReservation;
using Application.Interfaces;
using Application.Queries.GetAllReservationsPaged;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ReservationsController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReservation([FromBody] CreateReservationCommand request)
    {
        var result = await commandDispatcher.DispatchAsync<CreateReservationCommand, ApiResponse<CreateReservationCommandResponse>>(request);
        return Created($"/api/v1/reservations/{result}", result);
    }

    [HttpGet]
    public async Task<IActionResult> CreateReservation([FromQuery] GetAllReservationsQuery request)
    {
        var result = await queryDispatcher.DispatchAsync<GetAllReservationsQuery, PaginatedResponse<GetAllReservationsResponse>>(request);
        return Created($"/api/v1/reservations/{result}", result);
    }
}
