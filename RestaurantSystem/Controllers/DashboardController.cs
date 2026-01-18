using Application.Interfaces;
using Application.Queries.GetDashboard;
using Domain.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Api.Controllers;

[ApiController]
[Authorize]
[Route("[controller]")]
public class DashboardController(QueryDispatcher queryDispatcher) : ControllerBase
{
    [HttpGet("metrics")]
    public async Task<IActionResult> GetDashboard()
    {
        if (!Request.Headers.TryGetValue("X-Restaurant-Id", out var restaurantId))
            return Unauthorized("Missing restaurant id");

        var result = await queryDispatcher.DispatchAsync<GetDashboardQuery, ApiResponse<GetDashboardResponse>>(new GetDashboardQuery(int.Parse(restaurantId)));

        return Ok(result);
    }
}
