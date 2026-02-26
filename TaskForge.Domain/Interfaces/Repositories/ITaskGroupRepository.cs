
using TaskForge.Domain.Entities;

namespace TaskForge.Domain.Interfaces.Repositories;

public interface ITaskGroupRepository : IRepository<TaskGroup>
{
    Task<IEnumerable<TaskGroup>> GetByOrgIdAsync(Guid id);
}