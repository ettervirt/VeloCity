using FluentValidation;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using VeloCity.Api.Common.Exceptions;

namespace VeloCity.Api.Infrastructure.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken ct)
    {

        switch (exception)
        {
            case ValidationException validationException:
            {
                var problemDetails = new ValidationProblemDetails(
                    validationException.Errors
                        .GroupBy(e => e.PropertyName)
                        .ToDictionary(
                            g => g.Key,
                            g => g.Select(e => e.ErrorMessage).ToArray()
                        )
                )
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Detail = "One or more validation errors occurred"
                };

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);
                return true;
            }
            case AppException appException:
            {
                var problemDetails = new ProblemDetails
                {
                    Status = appException.StatusCode,
                    Title = "Business Error",
                    Detail = appException.Message
                };

                httpContext.Response.StatusCode = appException.StatusCode;
                await httpContext.Response.WriteAsJsonAsync(problemDetails, ct);
                return true;
            }
            default:
                logger.LogError(exception, "Unexpected error occured");
                return false;
        }
    }
}
