using TaskForge.Domain.Entities;

namespace TaskForge.Domain.Interfaces.Services;

public interface IOrganizationService
{
    Task<IEnumerable<Organization>> GetAllAsync();
    Task<Organization> GetWithTaskGroups(Guid id);
    Task<Organization> GetByIdAsync(Guid id);
    Task<Organization> AddAsync(Organization org);
    Task<Organization> UpdateAsync(Organization org);
}