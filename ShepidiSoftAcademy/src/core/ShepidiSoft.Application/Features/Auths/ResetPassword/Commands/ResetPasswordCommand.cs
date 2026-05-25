using MediatR;
using ShepidiSoft.Application.Features.Auths;

namespace ShepidiSoft.Application.Features.Auths.ResetPassword.Commands;

public sealed record ResetPasswordCommand(
    string Email,
    string Token,
    string NewPassword,
    string ConfirmPassword) : IRequest<ServiceResult>;
