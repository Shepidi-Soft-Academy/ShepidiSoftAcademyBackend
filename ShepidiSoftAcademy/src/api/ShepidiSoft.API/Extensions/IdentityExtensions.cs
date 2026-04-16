using Microsoft.AspNetCore.Identity;
using ShepidiSoft.Identity.Models;
using ShepidiSoft.Persistence.Context;

namespace ShepidiSoft.API.Extensions;

public static class IdentityExtensions
{
    public static IServiceCollection AddIdentityExt(this IServiceCollection services)
    {
        services.AddIdentity<ApplicationUser, IdentityRole<Guid>>(options =>
        {
            options.Password.RequireDigit = false;
            options.Password.RequireLowercase = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequiredLength = 6;
            options.Password.RequireUppercase = false;
            options.User.RequireUniqueEmail = true;
        })
        .AddEntityFrameworkStores<AppDbContext>();



        return services;
    }

}
