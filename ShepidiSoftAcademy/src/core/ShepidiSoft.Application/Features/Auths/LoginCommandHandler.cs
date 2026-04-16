using MediatR;
using ShepidiSoft.Application.Contracts.Identity.Auths;

namespace ShepidiSoft.Application.Features.Auths;

public sealed class LoginCommandHandler(
    IAuthService authService
) : IRequestHandler<LoginCommand, ServiceResult<LoginCommandResponse>>
{
    public async Task<ServiceResult<LoginCommandResponse>> Handle(
        LoginCommand request,
        CancellationToken cancellationToken)
    {
        return await authService.LoginAsync(request);
    }
}