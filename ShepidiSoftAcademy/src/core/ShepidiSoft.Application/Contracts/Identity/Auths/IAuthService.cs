using Microsoft.AspNetCore.Identity.Data;
using ShepidiSoft.Application.Features.Auths;

namespace ShepidiSoft.Application.Contracts.Identity.Auths;

public interface IAuthService
{
    Task<ServiceResult<LoginCommandResponse>> LoginAsync(LoginCommand request);
}