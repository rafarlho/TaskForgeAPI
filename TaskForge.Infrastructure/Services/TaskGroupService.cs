using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Domain.Interfaces.Services;
using TaskForge.Domain.Exceptions;

namespace TaskForge.Infrastructure.Services;

public class TaskGroupService : ITaskGroupService
{
    private readonly ITaskGroupRepository _repository;

    public TaskGroupService(ITaskGroupRepository repository)
    {
        _repository = repository;
    }

    public async Task<TaskGroup> AddAsync(TaskGroup org)
    {
        
        var orgsWithSameName = await _repository.GetByConditionAsync(x => x.Name == org.Name);
        
        if (orgsWithSameName.Any()) throw new DuplicateEntityException("TaskGroup", "Name", org.Name);

        return await _repository.AddAsync(org);
        
    }

    public async Task<IEnumerable<TaskGroup>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<TaskGroup> GetByIdAsync(Guid id)
    {

        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new EntityNotFoundException("TaskGroup", id);
        }

        return entity;
    }

    public async Task<TaskGroup> UpdateAsync(TaskGroup org)
    {
        if (org.Id == Guid.Empty)
            throw new ValidationException("Id", "TaskGroup ID cannot be empty");

        var storedOrg = await _repository.GetByIdAsync(org.Id);

        if (storedOrg is null)
            throw new EntityNotFoundException("TaskGroup", org.Id);

        if (!storedOrg.Version.SequenceEqual(org.Version))
            throw new ConcurrencyException("TaskGroup", org.Id);

        var orgsWithSameName =
            await _repository.GetByConditionAsync(x => x.Name == org.Name && x.Id != org.Id);

        if (orgsWithSameName.Any())
            throw new DuplicateEntityException("TaskGroup", "Name", org.Name);

        var updated = await _repository.UpdateAsync(org.Id, entity =>
        {
            entity.Name = org.Name;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Version = Guid.NewGuid().ToByteArray();
        });

        return updated!;
    }
}