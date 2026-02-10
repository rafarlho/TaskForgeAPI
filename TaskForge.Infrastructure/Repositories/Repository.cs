using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

    public async Task<T> AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity; 

    }

    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _dbSet 
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate)
    {
        return await _dbSet
            .Where(predicate)
            .AsNoTracking()
            .ToListAsync();
    }

    public Task<T?> GetByIdAsync(Guid id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    public async Task<T> UpdateAsync(T entity)
    {
        _dbSet.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }
}