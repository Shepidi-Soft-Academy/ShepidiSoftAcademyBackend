using Microsoft.Extensions.DependencyInjection;
using ShepidiSoft.BackgroundJobs.Outbox;

namespace ShepidiSoft.BackgroundJobs.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddBackgroundJobsExt(this IServiceCollection services)
    {
        services.AddHostedService<OutboxProcessorJob>();

        return services;
    }
}
