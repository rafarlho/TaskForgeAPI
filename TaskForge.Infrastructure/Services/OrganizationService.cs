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
        if (string.IsNullOrWhiteSpace(org.Name)) throw new ValidationException("Name", "Organization name cannot be empty");
        
        var orgsWithSameName = await _repository.GetByConditionAsync(x => x.Name == org.Name);
        
        if (orgsWithSameName.Any()) throw new DuplicateEntityException("Organization", "Name", org.Name);

        return await _repository.AddAsync(org);
        
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}