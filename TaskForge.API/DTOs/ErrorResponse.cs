namespace TaskForge.Api.DTOs
{
    public record ErrorResponse(
        string ErrorCode,
        string Message,
        DateTime Timestamp,
        string? TraceId = null,
        Dictionary<string, string[]>? Errors = null
    );
}
