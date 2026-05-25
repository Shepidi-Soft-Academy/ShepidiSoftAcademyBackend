using MediatR;
using ShepidiSoft.Application.Features.Auths;

namespace ShepidiSoft.Application.Features.Auths.ForgotPassword.Commands;

public sealed record ForgotPasswordCommand(string Email) : IRequest<ServiceResult>;
