using Microsoft.EntityFrameworkCore;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Infrastructure.Data;

namespace TaskForge.Infrastructure.Repositories;

public class Repository<T> : IRepository<T> where T : class
{
    protected readonly TaskForgeDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repository(TaskForgeDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet 
            .AsNoTracking()
            .ToListAsync();
    }
}