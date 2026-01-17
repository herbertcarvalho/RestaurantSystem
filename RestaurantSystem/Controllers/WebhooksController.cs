using Application.Commands.ConfirmReservation;
using Application.Interfaces;
using Domain.Common;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantSystem.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WebhooksController(
        CommandDispatcher commandDispatcher
        ) : ControllerBase
    {
        [HttpPost("payment")]
        public async Task<IActionResult> ConfirmReservationWebhookCommand([FromBody] ConfirmReservationWebhookCommand request)
        {
            if (!Request.Headers.TryGetValue("X-Signature", out var signatureHeader))
                return Unauthorized("Missing signature");

            var result = await commandDispatcher.DispatchAsync<ConfirmReservationWebhookCommand, ApiResponse<string>>(request);
            return Created($"/api/v1/reservations/{result}", result);
        }
    }
}
