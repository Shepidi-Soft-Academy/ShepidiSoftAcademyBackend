namespace ShepidiSoft.Application.Contracts.Identity;

public interface IRoleService
{
    Task<bool> EnsureRoleExistsAsync(string roleName);
    Task<bool> AssignRoleToUserAsync(Guid userId, string roleName);
    Task<IList<string>> GetUserRolesAsync(Guid userId);
}
