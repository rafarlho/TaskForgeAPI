using System.Net;
using System.Text.Json;
using TaskForge.Api.DTOs;
using TaskForge.Domain.Exceptions;

namespace TaskForge.Api.Middleware;

public class GlobalExceptionHandlerMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
    private readonly IHostEnvironment _environment;

    public GlobalExceptionHandlerMiddleware(
        RequestDelegate next,
        ILogger<GlobalExceptionHandlerMiddleware> logger,
        IHostEnvironment environment)
    {
        _next = next;
        _logger = logger;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private async Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        var (statusCode, errorResponse) = exception switch
        {
            ValidationException validationEx => (
                HttpStatusCode.BadRequest,
                new ErrorResponse(
                    ErrorCode: validationEx.ErrorCode,
                    Message: validationEx.Message,
                    Timestamp: DateTime.UtcNow,
                    TraceId: context.TraceIdentifier,
                    Errors: validationEx.Errors
                )
            ),

            DuplicateEntityException duplicateEx => (
                HttpStatusCode.Conflict,
                new ErrorResponse(
                    ErrorCode: duplicateEx.ErrorCode,
                    Message: duplicateEx.Message,
                    Timestamp: DateTime.UtcNow,
                    TraceId: context.TraceIdentifier
                )
            ),

            ConcurrencyException concurrencyEx => (
                HttpStatusCode.Conflict,
                new ErrorResponse(
                    ErrorCode: concurrencyEx.ErrorCode,
                    Message: concurrencyEx.Message,
                    Timestamp: DateTime.UtcNow,
                    TraceId: context.TraceIdentifier
                )
            ),

            EntityNotFoundException notFoundEx => (
                HttpStatusCode.NotFound,
                new ErrorResponse(
                    ErrorCode: notFoundEx.ErrorCode,
                    Message: notFoundEx.Message,
                    Timestamp: DateTime.UtcNow,
                    TraceId: context.TraceIdentifier
                )
            ),

            DomainException domainEx => (
                HttpStatusCode.BadRequest,
                new ErrorResponse(
                    ErrorCode: domainEx.ErrorCode,
                    Message: domainEx.Message,
                    Timestamp: DateTime.UtcNow,
                    TraceId: context.TraceIdentifier
                )
            ),

            _ => (
                HttpStatusCode.InternalServerError,
                new ErrorResponse(
                    ErrorCode: "INTERNAL_SERVER_ERROR",
                    Message: _environment.IsDevelopment()
                        ? exception.Message
                        : "An unexpected error occurred. Please try again later.",
                    Timestamp: DateTime.UtcNow,
                    TraceId: context.TraceIdentifier
                )
            )
        };

        _logger.LogError(
            exception,
            "Error occurred: {ErrorCode} - {Message}. TraceId: {TraceId}",
            errorResponse.ErrorCode,
            exception.Message,
            context.TraceIdentifier
        );

        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)statusCode;

        var options = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };

        await context.Response.WriteAsync(
            JsonSerializer.Serialize(errorResponse, options)
        );
    }
}