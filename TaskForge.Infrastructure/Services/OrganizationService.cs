using TaskForge.Domain.Entities;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Domain.Interfaces.Services;

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
        if (orgsWithSameName.Any()) throw new InvalidOperationException($"An organization with the name '{org.Name}' already exists.");

        return await _repository.AddAsync(org);
        
    }

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}