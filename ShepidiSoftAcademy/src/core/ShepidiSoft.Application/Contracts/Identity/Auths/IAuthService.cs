using ShepidiSoft.Application.Features.Auths;
using ShepidiSoft.Application.Features.Auths.ForgotPassword.Commands;
using ShepidiSoft.Application.Features.Auths.ResetPassword.Commands;

namespace ShepidiSoft.Application.Contracts.Identity.Auths;

public interface IAuthService
{
    Task<ServiceResult<LoginCommandResponse>> LoginAsync(LoginCommand request);
    Task<ServiceResult> ForgotPasswordAsync(ForgotPasswordCommand request);
    Task<ServiceResult> ResetPasswordAsync(ResetPasswordCommand request);
}