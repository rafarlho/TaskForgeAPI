namespace TaskForge.Api.DTOs;

public record CreateOrganizationDto(string Name);
public record UpdateOrganizationDto(string Name);

public record OrganizationResponseDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt,
    byte[] Version
);