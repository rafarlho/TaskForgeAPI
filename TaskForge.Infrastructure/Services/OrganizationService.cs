using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Domain.Interfaces.Services;
using TaskForge.Domain.Exceptions;

namespace TaskForge.Infrastructure.Services;

public class OrganizationService : IOrganizationService
{
    private readonly IOrganizationRepository _repository;

    public OrganizationService(IOrganizationRepository repository)
    {
        _repository = repository;
    }

    public async Task<Organization> AddAsync(Organization org)
    {
        
        var orgsWithSameName = await _repository.GetByConditionAsync(x => x.Name == org.Name);
        
        if (orgsWithSameName.Any()) throw new DuplicateEntityException("Organization", "Name", org.Name);

        return await _repository.AddAsync(org);
        
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }

    public async Task<Organization> GetByIdAsync(Guid id)
    {

        var entity = await _repository.GetByIdAsync(id);

        if (entity == null)
        {
            throw new EntityNotFoundException("Organization", id);
        }

        return entity;
    }

    public async Task<Organization> UpdateAsync(Organization org)
    {
        if (org.Id == Guid.Empty)
            throw new ValidationException("Id", "Organization ID cannot be empty");

        var storedOrg = await _repository.GetByIdAsync(org.Id);

        if (storedOrg is null)
            throw new EntityNotFoundException("Organization", org.Id);

        if (!storedOrg.Version.SequenceEqual(org.Version))
            throw new ConcurrencyException("Organization", org.Id);

        var orgsWithSameName =
            await _repository.GetByConditionAsync(x => x.Name == org.Name && x.Id != org.Id);

        if (orgsWithSameName.Any())
            throw new DuplicateEntityException("Organization", "Name", org.Name);

        var updated = await _repository.UpdateAsync(org.Id, entity =>
        {
            entity.Name = org.Name;
            entity.UpdatedAt = DateTime.UtcNow;
            entity.Version = Guid.NewGuid().ToByteArray();
        });

        return updated!;
    }
}