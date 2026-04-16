using ShepidiSoft.Application.Features.Auths;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Contracts.Identity;

public  interface IUserService
{
    Task<ServiceResult<bool>> IsExistAsync(Guid userId);
    Task<ServiceResult<CreateUserRequestResponse>> CreateAsync(CreateUserRequest request);
    Task<ServiceResult<UserDtoForLogin>> GetByUserNameAsync(string userName);
    Task<bool> CheckPasswordAsync(string userName, string password);
    Task<ServiceResult> UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime refreshTokenExpires);
    Task<IReadOnlyList<string>> GetRolesAsync(Guid userId);
    Task<ServiceResult>UpdatePasswordAsync(Guid userId, string oldPassword,string newPassword,string confirmNewPassword);
    Task<ServiceResult> DeleteAsync(Guid userId);
    Task<ServiceResult> UpdateAsync(Guid userId);
    Task<ServiceResult<List<UserDto>>> GetUsersByIdsAsync(List<Guid> userIds, CancellationToken cancellationToken);
    Task<ServiceResult<UserDto>> GetByIdAsync(Guid userId, CancellationToken cancellationToken);







}
