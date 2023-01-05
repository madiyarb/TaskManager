using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskManager.Application.Contracts;
using TaskManager.Infrastucture.Persistance;
using TaskManager.Infrastucture.Repositories;

namespace TaskManager.Infrastucture;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<TaskDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("TaskConnectionString")));

        services.AddScoped<ITaskRepository, TaskRepository>();
        return services;
    }
}