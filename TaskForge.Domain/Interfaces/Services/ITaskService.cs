using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Domain.Interfaces.Services;

public interface ITaskService
{
    Task<IEnumerable<Task>> GetAllAsync();
    Task<Task> GetByIdAsync(Guid id);
    Task<Task> AddAsync(Task org);
    Task<Task> UpdateAsync(Task org);
}