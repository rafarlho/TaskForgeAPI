using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Domain.Interfaces.Services;
using TaskForge.Domain.Exceptions;

namespace TaskForge.Infrastructure.Services;

public class TaskGroupService : ITaskGroupService
{
    private readonly ITaskGroupRepository _repository;
    private readonly IOrganizationRepository _organizationRepository;

    public TaskGroupService(ITaskGroupRepository repository, IOrganizationRepository organizationRepository)
    {
        _repository = repository;
        _organizationRepository = organizationRepository;
    }

    public async Task<TaskGroup> AddAsync(TaskGroup tg)
    {
        
        var orgsWithSameName = await _repository.GetByConditionAsync(x => x.Name == tg.Name);
        
        if (orgsWithSameName.Any()) throw new DuplicateEntityException("TaskGroup", "Name", tg.Name);

        return await _repository.AddAsync(tg);
        
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

    public async Task<TaskGroup> UpdateAsync(TaskGroup tg)
    {
        if (tg.Id == Guid.Empty)
            throw new ValidationException("Id", "TaskGroup ID cannot be empty");

        var storedTg = await _repository.GetByIdAsync(tg.Id);

        if (storedTg is null)
            throw new EntityNotFoundException("TaskGroup", tg.Id);

        if (!storedTg.Version.SequenceEqual(tg.Version))
            throw new ConcurrencyException("TaskGroup", tg.Id);

        var tgsWithSameName =
            await _repository.GetByConditionAsync(x => x.Name == tg.Name && x.Id != tg.Id);

        if (tgsWithSameName.Any())
            throw new DuplicateEntityException("TaskGroup", "Name", tg.Name);

        var updated = await _repository.UpdateAsync(tg.Id, entity => 
        {
            entity.Name = tg.Name;
            entity.OrganizationId = tg.OrganizationId;
            entity.Description = tg.Description;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Version = Guid.NewGuid().ToByteArray();
            
        });

        return updated!;
    }
}