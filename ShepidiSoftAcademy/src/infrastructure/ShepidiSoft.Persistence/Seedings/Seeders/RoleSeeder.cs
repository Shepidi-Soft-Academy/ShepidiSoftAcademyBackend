using ShepidiSoft.Application.Contracts.Identity;

namespace ShepidiSoft.Persistence.Seedings.Seeders;

public class RoleSeeder(IRoleService roleService)
{
    public async Task SeedAsync()
    {
        var roles = new[] { "Admin", "Instructor", "Student", "Manager" };

        foreach (var role in roles)
        {
            await roleService.EnsureRoleExistsAsync(role);
        }
    }
}
