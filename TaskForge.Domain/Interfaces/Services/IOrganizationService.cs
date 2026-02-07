using TaskForge.Domain.Entities;

namespace TaskForge.Domain.Interfaces.Services;

public interface IOrganizationService
{
    Task<IEnumerable<Organization>> GetAllAsync();
}