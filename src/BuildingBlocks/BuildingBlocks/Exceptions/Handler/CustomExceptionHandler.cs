using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace BuildingBlocks.Exceptions.Handler;

public class CustomExceptionHandler
    (ILogger<CustomExceptionHandler> logger)
    : IExceptionHandler
{

    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(
            "Error Message: {exceptionMessage}, Time of accurence {time}",
            exception.Message, DateTime.UtcNow);

        (string Details, string Titles, int StatusCode) details = exception switch
        {
            InternalServerException =>
            (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            ),

            ValidationException =>
            (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),

            BadRequestException =>
            (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status400BadRequest
            ),

            NotFoundException =>
            (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status404NotFound
            ),

            _ =>
            (
              exception.Message,
              exception.GetType().Name,
              httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError
            )
        };

        var problemDetails = new ProblemDetails
        {
            Title = details.Titles,
            Detail = details.Details,
            Status = details.StatusCode,
            Instance = httpContext.Request.Path
        };

        problemDetails.Extensions.Add("traceId", httpContext.TraceIdentifier);

        if (exception is ValidationException validationException)
        {
            problemDetails.Extensions.Add("ValidationError", validationException.Errors);
        }

        await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

        return true;
    }
}