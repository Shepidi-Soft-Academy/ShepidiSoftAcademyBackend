using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Identity.Auths;
using ShepidiSoft.Application.Contracts.Identity.Auths.Jwt;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Auths;
using ShepidiSoft.Application.Features.Auths.ForgotPassword.Commands;
using ShepidiSoft.Application.Features.Auths.ResetPassword.Commands;
using ShepidiSoft.Application.Features.Outbox;
using ShepidiSoft.Domain.Entities;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json;

namespace ShepidiSoft.Identity.Services;

public sealed class AuthService(
    IUserService userService, 
    IRoleService roleService, 
    IJwtProvider jwtProvider,
    IOutboxRepository outboxRepository) : IAuthService
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

    public async Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordCommand request)
    {
        var tokenResult = await userService.GeneratePasswordResetTokenAsync(request.Email);
        
        if (!tokenResult.IsSuccess)
        {            
            return ServiceResult.Fail(tokenResult.ErrorMessage, System.Net.HttpStatusCode.NotFound);
        }

        // Email Outbox işlemleri
        var resetLink = $"http://localhost:5173/reset-password?email={Uri.EscapeDataString(request.Email)}&token={Uri.EscapeDataString(tokenResult.Data!)}";
        var payload = new EmailOutboxPayload
        {
            To = request.Email,
            Subject = "Şifre Sıfırlama İsteği",
            TemplateName = "PasswordReset",
            Variables = new Dictionary<string, string>
            {
                { "ResetLink", resetLink }
            }
        };

        var outboxMessage = new OutboxMessage
        {
            Type = "Email",
            Payload = JsonSerializer.Serialize(payload)
        };

        await outboxRepository.AddAsync(outboxMessage);
        await outboxRepository.SaveChangesAsync();

        return ServiceResult.Success();
    }

    public async Task<ServiceResult> ResetPasswordAsync(ResetPasswordCommand request)
    {
        if (request.NewPassword != request.ConfirmPassword)
            return ServiceResult.Fail("Şifreler eşleşmiyor", System.Net.HttpStatusCode.BadRequest);

        return await userService.ResetPasswordWithCustomTokenAsync(request.Email, request.Token, request.NewPassword);
    }

    private string CreateRefreshToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
    }
}