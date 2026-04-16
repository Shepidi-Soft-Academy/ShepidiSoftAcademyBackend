using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Identity.Auths;
using ShepidiSoft.Application.Contracts.Identity.Auths.Jwt;
using ShepidiSoft.Identity.Auths.Jwt;
using ShepidiSoft.Identity.Services;

namespace ShepidiSoft.Identity;

public static class DependencyInjection
{

    public static IServiceCollection AddUserExt(this IServiceCollection services, IConfiguration configuration)
    {

        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IJwtProvider, JwtProvider>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IRoleService, RoleService>();





        return services;
    }

}
