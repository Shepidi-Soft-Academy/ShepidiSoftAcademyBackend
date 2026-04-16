using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using ShepidiSoft.Application.Contracts.Common;
using System.Reflection;

namespace ShepidiSoft.Application.Extensions;

public static class DependencyInjection
{

    public static IServiceCollection AddApplicationExt(this IServiceCollection services)
    {
        MediatrExt(services);
        services.AddFluentValidationAutoValidation();

        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddScoped<ICurrentUserService, CurrentUserService>();



        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());

        return services;
    }

    public static IServiceCollection MediatrExt(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly())
        );
       
        return services;
    }
}
