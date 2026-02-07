namespace TaskForge.Api.DTOs;

public record CreateOrganizaionDto(string Name);
public record UpdaeOrganizaionDto(string Name);

public record OrganizationResponseDto(
    Guid Id,
    string Name,
    DateTime CreatedAt,
    DateTime UpdatedAt
);