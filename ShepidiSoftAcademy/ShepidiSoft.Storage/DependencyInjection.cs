using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShepidiSoft.Application.Contracts;

namespace ShepidiSoft.Storage;

public static class DependencyInjection
{
    public static IServiceCollection AddStorageExt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IFileStorageService, FileStorageService>();

        return services;
    }

}
