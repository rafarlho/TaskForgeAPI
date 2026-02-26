
using TaskForge.Domain.Entities;

namespace TaskForge.Domain.Interfaces.Repositories;

public interface IOrganizationRepository : IRepository<Organization>
{
    Task<Organization> GetWithTaskGroups(Guid id);
}