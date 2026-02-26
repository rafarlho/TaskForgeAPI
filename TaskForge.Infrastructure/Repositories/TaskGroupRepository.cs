using Microsoft.EntityFrameworkCore;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Infrastructure.Data;

namespace TaskForge.Infrastructure.Repositories;

public class TaskGroupRepository : Repository<TaskGroup>, ITaskGroupRepository
{
    public TaskGroupRepository(TaskForgeDbContext context): base(context)
    {
        
    }

    public async Task<IEnumerable<TaskGroup>> GetByOrgIdAsync(Guid id)
    {
        return await _context.TaskGroups
            .Where(tg => tg.OrganizationId == id)
            .Include(tg => tg.Organization)
            .ToListAsync();
    }
}