using MediatR;
using ShepidiSoft.Application.Contracts.Identity.Auths;
using ShepidiSoft.Application.Features.Auths.ForgotPassword.Commands;

namespace ShepidiSoft.Application.Features.Auths.ForgotPassword.Queries;

public sealed class ForgotPasswordCommandHandler(IAuthService authService)
    : IRequestHandler<ForgotPasswordCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
    {
        return await authService.ForgotPasswordAsync(request);
    }
}