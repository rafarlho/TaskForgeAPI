using Microsoft.EntityFrameworkCore;
using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Infrastructure.Data;

namespace TaskForge.Infrastructure.Repositories;

public class OrganizationRepository : Repository<Organization>, IOrganizationRepository
{
    public OrganizationRepository(TaskForgeDbContext context): base(context)
    {
        
    }

    public async Task<Organization> GetWithTaskGroups(Guid id)
    {
        return await _context.Organizations
            .Include(o => o.TaskGroups)
            .FirstOrDefaultAsync(o => o.Id == id);
    }
}