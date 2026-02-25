using System.Linq.Expressions;

namespace TaskForge.Domain.Interfaces.Repositories;

public interface IRepository<T> where T: class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(Guid id);
    Task<T> AddAsync(T entity);
    Task<T> UpdateAsync(Guid id, Action<T> updateAction);
    Task<IEnumerable<T>> GetByConditionAsync(Expression<Func<T, bool>> predicate);
}