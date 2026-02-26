using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Domain.Interfaces.Services;
using TaskForge.Domain.Exceptions;
using Task = TaskForge.Domain.Entities.Task;

namespace TaskForge.Infrastructure.Services;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private readonly IOrganizationRepository _organizationRepository;

    public TaskService(ITaskRepository repository, IOrganizationRepository organizationRepository)
    {
        _repository = repository;
        _organizationRepository = organizationRepository;
    }

    public async Task<Task> AddAsync(Task task)
    {
        
        var tasksWithSameName = await _repository.GetByConditionAsync(x => x.Name == task.Name);
        
        if (tasksWithSameName.Any()) throw new DuplicateEntityException("Task", "Name", task.Name);

        return await _repository.AddAsync(task);
        
    }

    public async Task<IEnumerable<Task>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Task> GetByIdAsync(Guid id)
    {

        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Task", id);
        }

        return entity;
    }

    public async Task<Task> UpdateAsync(Task task)
    {
        if (task.Id == Guid.Empty)
            throw new ValidationException("Id", "Task ID cannot be empty");

        var storedTask = await _repository.GetByIdAsync(task.Id);

        if (storedTask is null)
            throw new EntityNotFoundException("Task", task.Id);

        if (!storedTask.Version.SequenceEqual(task.Version))
            throw new ConcurrencyException("Task", task.Id);

        var tasksWithSameName =
            await _repository.GetByConditionAsync(x => x.Name == task.Name && x.Id != task.Id);

        if (tasksWithSameName.Any())
            throw new DuplicateEntityException("Task", "Name", task.Name);

        var updated = await _repository.UpdateAsync(task.Id, entity => 
        {
            entity.Name = task.Name;
            entity.TaskGroupId = task.TaskGroupId;
            entity.Description = task.Description;
            entity.StationName = task.StationName;
            entity.Assignee = task.Assignee;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Version = Guid.NewGuid().ToByteArray();
            
        });

        return updated!;
    }
}