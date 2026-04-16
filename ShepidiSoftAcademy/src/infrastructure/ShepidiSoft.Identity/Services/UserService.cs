using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Features.Auths;
using ShepidiSoft.Application.Features.Users.Dtos;
using ShepidiSoft.Identity.Models;
using System.Net;

namespace ShepidiSoft.Identity.Services;

public sealed class UserService(UserManager<ApplicationUser> userManager) : IUserService
{
    public async Task<ServiceResult<CreateUserRequestResponse>> CreateAsync(CreateUserRequest request)
    {
        var existByEmail = await userManager.FindByEmailAsync(request.Email);

        if (existByEmail != null)
            return ServiceResult<CreateUserRequestResponse>.Fail("Email Mevcut!");



        var existPhoneNumber = await CheckIsPhoneNumberExist(request.PhoneNumber);

        if (existPhoneNumber.Data)
            return ServiceResult<CreateUserRequestResponse>.Fail("Bu telefon numarası sisteme kayıtlı!");

        var user = new ApplicationUser
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            DateOfBirth = request.DateOfBirth,
            UserName = request.Email,
            Email = request.Email,
            PhoneNumber = request.PhoneNumber,
            PhotoUrl = request.PhotoUrl,
            LinkednUrl = request.LinkednUrl,
            YoutubeUrl= request.YoutubeUrl,
            GithubUrl= request.GithubUrl,
            
           
        };

        var result = await userManager.CreateAsync(user, request.PhoneNumber);

        if (!result.Succeeded)
        {

            var errors = result.Errors.Select(x => x.Description).ToList();
            return ServiceResult<CreateUserRequestResponse>.Fail(errors);
        }

        // Response oluştur
        var response = new CreateUserRequestResponse(
            user.Id

        );

        return ServiceResult<CreateUserRequestResponse>.Success(response);

    }

    public async Task<ServiceResult<bool>> IsExistAsync(Guid userId)
    {
        var response = await userManager.Users.AnyAsync(u => u.Id == userId);

        return ServiceResult<bool>.Success(response);

    }

  
    public async Task<ServiceResult<bool>> CheckIsPhoneNumberExist(string phoneNumber)
    {
        var exists = await userManager.Users
            .AnyAsync(x => x.PhoneNumber == phoneNumber);

        return ServiceResult<bool>.Success(exists);
    }

    public async Task<ServiceResult<UserDtoForLogin>> GetByUserNameAsync(string userName)
    {
        var user = await userManager.FindByNameAsync(userName);

        if (user is null)
            return ServiceResult<UserDtoForLogin>.Fail("User not found", HttpStatusCode.NotFound);

        var userDto = new UserDtoForLogin(
            UserId: user.Id,
            Mail: user.Email ?? "",
            UserName: user.UserName ?? "",
            Refreshtoken: user.RefreshToken ?? "",
            RefreshtokenExpires: user.RefreshTokenExpires ?? DateTime.UtcNow.AddDays(7)

        );

        return ServiceResult<UserDtoForLogin>.Success(userDto);
    }

    public async Task<bool> CheckPasswordAsync(string userName, string password)
    {
        var user = await userManager.FindByNameAsync(userName);
        return await userManager.CheckPasswordAsync(user!, password);
    }
    public async Task<ServiceResult> UpdateRefreshTokenAsync(Guid userId, string refreshToken, DateTime refreshTokenExpires)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return ServiceResult.Fail("User not found", HttpStatusCode.NotFound);

        user.RefreshToken = refreshToken;
        user.RefreshTokenExpires = refreshTokenExpires;

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
            return ServiceResult.Fail(
                string.Join(", ", result.Errors.Select(e => e.Description)),
                HttpStatusCode.BadRequest);

        return ServiceResult.Success();
    }

    public async Task<IReadOnlyList<string>> GetRolesAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null) return new List<string>();

        var roles = await userManager.GetRolesAsync(user);
        return roles.ToList();
    }

    public async Task<ServiceResult> UpdatePasswordAsync(
      Guid userId,
      string oldPassword,
      string newPassword,
      string confirmNewPassword)
    {
        if (newPassword != confirmNewPassword)
            return ServiceResult.Fail("Girilen şifreler eşleşmiyor!", HttpStatusCode.BadRequest);

        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user is null)
            return ServiceResult.Fail("Kullanıcı Bulunamadı!", HttpStatusCode.NotFound);

        var result = await userManager.ChangePasswordAsync(user, oldPassword, newPassword);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(e => e.Code switch
            {
                "PasswordMismatch" => "Eski şifre hatalı.",
                _ => e.Description
            });

            var errorMessage = string.Join(", ", errors);
            return ServiceResult.Fail(errorMessage, HttpStatusCode.BadRequest);
        }

        await userManager.UpdateSecurityStampAsync(user);

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> DeleteAsync(Guid userId)
    {
        var user = await userManager.FindByIdAsync(userId.ToString());

        if (user is null)
            return ServiceResult.Fail("Kullanıcı bulunamadı!", HttpStatusCode.NotFound);

        var result = await userManager.DeleteAsync(user);

        if (!result.Succeeded)
        {
            var errors = result.Errors.Select(x => x.Description).ToList();
            return ServiceResult.Fail(errors);
        }

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }

    public Task<ServiceResult> UpdateAsync(Guid userId)
    {
        throw new NotImplementedException();
    }
// UserService implementasyonu
    public async Task<ServiceResult<List<UserDto>>> GetUsersByIdsAsync(List<Guid> userIds, CancellationToken cancellationToken)
    {
        var users = await userManager.Users
            .Where(u => userIds.Contains(u.Id))
            .Select(u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email ?? "",u.PhotoUrl,u.LinkednUrl))
            .ToListAsync(cancellationToken);

        return ServiceResult<List<UserDto>>.Success(users);
    }

    public async Task<ServiceResult<UserDto>> GetByIdAsync(Guid userId, CancellationToken cancellationToken)
    {
        var user = await userManager.Users
            .Where(x => x.Id == userId)
            .Select(x => new UserDto(
                x.Id,
                x.FirstName,
                x.LastName,
                x.Email!,
                x.PhotoUrl,
                x.LinkednUrl
            ))
            .FirstOrDefaultAsync(cancellationToken);

        if (user is null)
            return ServiceResult<UserDto>.Fail("Kullanıcı bulunamadı");

        return ServiceResult<UserDto>.Success(user);
    }
}
