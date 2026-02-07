using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskForge.Domain.Interfaces.Repositories;
using TaskForge.Domain.Interfaces.Services;
using TaskForge.Infrastructure.Data;
using TaskForge.Infrastructure.Repositories;
using TaskForge.Infrastructure.Services;

namespace TaskForge.Infrastructure;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration
    )
    {
        services.AddDbContext<TaskForgeDbContext>(options => {
            options.UseSqlServer(
                configuration.GetConnectionString("DefaultConnection"),
                b => b.MigrationsAssembly(typeof(TaskForgeDbContext).Assembly.FullName));
        });

        // Repositories
        services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        services.AddScoped<IOrganizationRepository, OrganizationRepository>();

        // Services
        services.AddScoped<IOrganizationService, OrganizationService>();

        return services;
    }
}