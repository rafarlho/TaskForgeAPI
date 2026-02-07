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

    public async Task<IEnumerable<Organization>> GetAllAsync()
    {
        return await _repository.GetAllAsync();
    }
}