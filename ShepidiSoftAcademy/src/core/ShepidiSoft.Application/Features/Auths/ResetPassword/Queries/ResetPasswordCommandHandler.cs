using MediatR;
using ShepidiSoft.Application.Contracts.Identity.Auths;
using ShepidiSoft.Application.Features.Auths.ResetPassword.Commands;

namespace ShepidiSoft.Application.Features.Auths.ResetPassword.Queries;

public sealed class ResetPasswordCommandHandler(IAuthService authService)
    : IRequestHandler<ResetPasswordCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        return await authService.ResetPasswordAsync(request);
    }
}
