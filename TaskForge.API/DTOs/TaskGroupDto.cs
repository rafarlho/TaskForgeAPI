using TaskForge.Api.DTOs;

public record CreateTaskGroupDto(string Name, string Description, Guid OrganizationId);
public record UpdateTaskGroupDto(Guid Id, string Name, string Description, byte[] Version, Guid OrganizationId);

public record TaskGroupResponseDto(
    Guid Id,
    string Name,
    string Description,
    Guid OrganizationId,
    IEnumerable<TaskResponseDto> Tasks,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    byte[] Version
);