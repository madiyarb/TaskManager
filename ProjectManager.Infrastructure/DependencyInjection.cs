using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ProjectManager.Application.Contracts;
using ProjectManager.Infrastructure.Persistance;
using ProjectManager.Infrastructure.Repositories;

namespace ProjectManager.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<ProjectDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("ProjectConnectionString")));

        services.AddScoped<IProjectRepository, ProjectRepository>();
        return services;
    }
}