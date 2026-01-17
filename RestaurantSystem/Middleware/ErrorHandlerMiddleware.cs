using Domain.Common;
using Domain.Exceptions;
using System.Net;
using System.Text.Json;

namespace RestaurantSystem.Api.Middleware;

public class ErrorHandlerMiddleware(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception error)
        {
            var response = context.Response;
            response.ContentType = "application/json";
            var responseModel = ApiResponse<string>.Fail(error.Message);

            switch (error)
            {
                case InvalidActionException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case FluentValidation.ValidationException:
                    response.StatusCode = (int)HttpStatusCode.BadRequest;
                    break;

                case NotFoundException:
                    response.StatusCode = (int)HttpStatusCode.NotFound;
                    break;

                default:
                    response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    responseModel.Message = "Sorry we cant process this now.";
                    break;
            }
            var result = JsonSerializer.Serialize(responseModel);

            await response.WriteAsync(result);
        }
    }
}