using System.Globalization;
using Microsoft.Extensions.Localization;
using PC28168.U202422620.Openpay.Platform.Resources;
using PC28168.U202422620.Openpay.Platform.Settlements.Domain.Exceptions;
using PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST.Resources;

namespace PC28168.U202422620.Openpay.Platform.Settlements.Interfaces.REST;

public sealed class ExceptionHandlingMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(
        HttpContext context,
        IStringLocalizer<ErrorMessages> localizer,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception) when (!context.Response.HasStarted)
        {
            var (statusCode, messageKey) = exception switch
            {
                DuplicateOrderTrackingCodeException => (StatusCodes.Status409Conflict, "DuplicateOrderTrackingCode"),
                ArgumentException => (StatusCodes.Status400BadRequest, "InvalidRequest"),
                _ => (StatusCodes.Status500InternalServerError, "UnexpectedError")
            };

            if (statusCode == StatusCodes.Status500InternalServerError)
            {
                logger.LogError(exception, "An unexpected error occurred while processing the request.");
            }

            context.Response.Clear();
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";
            context.Response.Headers.ContentLanguage = CultureInfo.CurrentUICulture.Name;
            await context.Response.WriteAsJsonAsync(new ErrorResource(localizer[messageKey].Value));
        }
    }
}
