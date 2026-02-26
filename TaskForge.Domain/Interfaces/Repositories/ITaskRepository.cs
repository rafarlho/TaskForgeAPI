
using TaskForge.Domain.Entities;
using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Domain.Interfaces.Repositories;

public interface ITaskRepository : IRepository<Task>
{
}