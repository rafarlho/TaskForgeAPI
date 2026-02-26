using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Infrastructure.Data;
using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Infrastructure.Repositories;

public class TaskRepository : Repository<Task>, ITaskRepository
{
    public TaskRepository(TaskForgeDbContext context): base(context)
    {
        
    }
}