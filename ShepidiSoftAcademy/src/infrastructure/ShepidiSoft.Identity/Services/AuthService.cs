using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Identity.Auths;
using ShepidiSoft.Application.Contracts.Identity.Auths.Jwt;
using ShepidiSoft.Application.Features.Auths;
using System.Security.Claims;
using System.Security.Cryptography;

namespace ShepidiSoft.Identity.Services;


public sealed class AuthService(IUserService userService, IRoleService roleService, IJwtProvider jwtProvider) : IAuthService
{
    public async Task<ServiceResult<LoginCommandResponse>> LoginAsync(LoginCommand request)
    {
        var user = await userService.GetByUserNameAsync(request.Email);
        if (!user.IsSuccess || user.Data is null)
            return ServiceResult<LoginCommandResponse>.Fail("Kullanıcı Bulunamadı");

        if (!await userService.CheckPasswordAsync(user.Data.UserName, request.Password))
            return ServiceResult<LoginCommandResponse>.Fail("Hatalı Parola");

        var claims = new List<Claim>
    {
        new Claim(ClaimTypes.NameIdentifier, user.Data.UserId.ToString()),
        new Claim(ClaimTypes.Email, user.Data.Mail),
    };

        var roles = await roleService.GetUserRolesAsync(user.Data.UserId);
        foreach (var role in roles)
        {
            claims.Add(new Claim(ClaimTypes.Role, role));
        }

        string accessToken = await jwtProvider.CreateTokenAsync(claims);

        string refreshToken = CreateRefreshToken();
        DateTime refreshTokenExpires = DateTime.UtcNow.AddDays(7);

        var updateResult = await userService.UpdateRefreshTokenAsync(
            user.Data.UserId,
            refreshToken,
            refreshTokenExpires);

        if (!updateResult.IsSuccess)
            return ServiceResult<LoginCommandResponse>.Fail("Token güncellemesi başarısız");


        return ServiceResult<LoginCommandResponse>.Success(
            new LoginCommandResponse(accessToken, refreshToken, refreshTokenExpires));
    }


    private string CreateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}