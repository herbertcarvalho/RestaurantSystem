
using Application.Commands.CheckInCustomer;
using Application.Commands.ConfirmReservation;
using Application.Commands.CreateReservation;
using Application.Interfaces;
using Application.Queries.GetAllReservationsPaged;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Api.Controllers;

[ApiController]
[Authorize]
[Route("api/v1/[controller]")]
public class ReservationsController(CommandDispatcher commandDispatcher, QueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> CreateReservationCommand([FromBody] CreateReservationCommand request)
    {
        var result = await commandDispatcher.DispatchAsync<CreateReservationCommand, ApiResponse<CreateReservationCommandResponse>>(request);
        return Created($"/api/v1/reservations/{result}", result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllReservationsQuery([FromQuery] GetAllReservationsQuery request)
    {
        var result = await queryDispatcher.DispatchAsync<GetAllReservationsQuery, PaginatedResponse<GetAllReservationsResponse>>(request);
        return Ok(result);
    }

    [HttpPut("{id}/confirm")]
    public async Task<IActionResult> ConfirmReservationCommand(int id, [FromBody] ConfirmReservationCommand request)
    {
        var result = await commandDispatcher.DispatchAsync<ConfirmReservationCommand, ApiResponse<string>>(id, request);
        return Ok(result);
    }

    [HttpPut("{id}/check-in")]
    public async Task<IActionResult> CustomerCheckInCommand(int id, [FromBody] CustomerCheckInCommand request)
    {
        var result = await commandDispatcher.DispatchAsync<CustomerCheckInCommand, ApiResponse<string>>(id, request);
        return Ok(result);
    }
}
