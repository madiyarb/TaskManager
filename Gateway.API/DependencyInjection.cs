using ExtendedHttpClient.Extensions;
using Gateway.API.Services;
using Gateway.API.Services.Inerfaces;
using Microsoft.OpenApi.Models;

namespace Gateway.API;

public static class DependencyInjection
{
    
    public static IServiceCollection AddHttpClients(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExtendedHttpClient();
        services.AddServiceWithExtendedHttpClient<IProjectService, ProjectService>(configuration["ApiSettings:ProjectUrl"]);
        return services;
    }
    
    public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Gateway", Version = "v1" });
            c.EnableAnnotations();
        });
        return services;
    }
}