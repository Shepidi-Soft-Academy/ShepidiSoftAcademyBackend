using Microsoft.AspNetCore.Identity;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Identity.Models;

namespace ShepidiSoft.Identity.Services;

public sealed class RoleService : IRoleService
{
    private readonly RoleManager<IdentityRole<Guid>> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public RoleService(
        RoleManager<IdentityRole<Guid>> roleManager,
        UserManager<ApplicationUser> userManager)
    {
        _roleManager = roleManager;
        _userManager = userManager;
    }
    public async Task<bool> EnsureRoleExistsAsync(string roleName)
    {
        if (!await _roleManager.RoleExistsAsync(roleName))
        {
            var createResult = await _roleManager.CreateAsync(new IdentityRole<Guid>(roleName));
            return createResult.Succeeded;
        }

        return true;
    }

    public async Task<bool> AssignRoleToUserAsync(Guid userId, string roleName)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null) return false;

        var result = await _userManager.AddToRoleAsync(user, roleName);
        return result.Succeeded;
    }

    public async Task<IList<string>> GetUserRolesAsync(Guid userId)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return new List<string>();

        return await _userManager.GetRolesAsync(user);
    }
}