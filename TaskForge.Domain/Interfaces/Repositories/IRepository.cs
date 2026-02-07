namespace TaskForge.Domain.Interfaces.Repositories;

public interface IRepository<T> where T: class
{
    Task<IEnumerable<T>> GetAllAsync();
}