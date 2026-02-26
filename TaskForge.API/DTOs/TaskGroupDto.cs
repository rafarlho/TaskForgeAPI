namespace TaskForge.Api.DTOs;

public record CreateOrganizationDto(string Name);
public record UpdateOrganizationDto(Guid Id, string Name, byte[] Version);

public record OrganizationResponseDto(
    Guid Id,
    string Name,
    IEnumerable<TaskGroupResponseDto> TaskGroups,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    byte[] Version
);