using TaskForge.Domain.Enums;

namespace TaskForge.Api.DTOs;

public record CreateTaskDto(string Name, string? Descrition, string? Assignee, Guid TaskGroupId);
public record UpdateTaskDto(Guid Id, string? Descrition,string? Assignee, Guid TaskGroupId, string Name, byte[] Version, Status Status);

public record TaskResponseDto(
    Guid Id,
    Guid TaskGroupId,
    string Name,
    string? Description,
    string? StationName,
    string? Assignee,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    Status Status,
    byte[] Version
);