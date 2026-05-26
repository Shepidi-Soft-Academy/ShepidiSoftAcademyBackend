using MediatR;
using ShepidiSoft.Application.Contracts.Identity.Auths;

namespace ShepidiSoft.Application.Features.Auths.ResetPassword.Commands;

public sealed class ResetPasswordCommandHandler(IAuthService authService)
    : IRequestHandler<ResetPasswordCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await authService.ResetPasswordAsync(request);
    }
}
