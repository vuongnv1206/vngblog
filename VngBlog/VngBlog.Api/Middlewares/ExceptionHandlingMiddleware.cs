using System.Text.Json;
using VngBlog.Domain.Exceptions;
using VngBLog.Application.Common;
using static VngBLog.Application.Common.ValidationException;

namespace VngBlog.Api.Middlewares;

internal sealed class ExceptionHandlingMiddleware : IMiddleware
{
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger)
        => _logger = logger;

    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (Exception e)
        {
            _logger.LogError(e, e.Message);

            await HandleExceptionAsync(context, e);
        }
    }

    private static async Task HandleExceptionAsync(HttpContext httpContext, Exception exception)
    {
        var statusCode = GetStatusCode(exception);

        var response = new
        {
            title = "Server Errors",
            status = statusCode,
            detail = exception.Message,
            errors = GetErrors(exception),
        };

        httpContext.Response.ContentType = "application/json";

        httpContext.Response.StatusCode = statusCode;

        await httpContext.Response.WriteAsync(JsonSerializer.Serialize(response));
    }

    private static int GetStatusCode(Exception exception) =>
        exception switch
        {
            NotFoundException => StatusCodes.Status404NotFound,
            ValidationException => StatusCodes.Status400BadRequest,
            FormatException => StatusCodes.Status422UnprocessableEntity,
            _ => StatusCodes.Status500InternalServerError
        };


    private static IReadOnlyCollection<ValidationError> GetErrors(Exception exception)
    {
        //IReadOnlyCollection<ValidationError> errors = null;

        //if (exception is ValidationException validationException)
        //{
        //    errors = validationException.Errors;
        //}

        //return errors;

        var errors = new List<ValidationError>();

        if (exception is ValidationException validationException)
        {
            foreach (var error in validationException.Errors)
            {
                foreach (var errorMessage in error.Value)
                {
                    errors.Add(new ValidationError(error.Key, errorMessage));
                }
            }
        }

        return errors;
    }


}
