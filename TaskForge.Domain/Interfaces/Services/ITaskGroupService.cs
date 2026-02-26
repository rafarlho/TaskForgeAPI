using TaskForge.Domain.Entities;

namespace TaskForge.Domain.Interfaces.Services;

public interface ITaskGroupService
{
    Task<IEnumerable<TaskGroup>> GetAllAsync();
    Task<TaskGroup> GetByIdAsync(Guid id);
    Task<TaskGroup> AddAsync(TaskGroup org);
    Task<TaskGroup> UpdateAsync(TaskGroup org);
}