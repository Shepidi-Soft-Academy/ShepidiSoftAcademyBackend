using MediatR;
using ShepidiSoft.Application.Contracts.Identity.Auths;

namespace ShepidiSoft.Application.Features.Auths.ForgotPassword.Commands;

public sealed class ForgotPasswordCommandHandler(IAuthService authService)
    : IRequestHandler<ForgotPasswordCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        return await authService.ForgotPasswordAsync(request);
    }
}