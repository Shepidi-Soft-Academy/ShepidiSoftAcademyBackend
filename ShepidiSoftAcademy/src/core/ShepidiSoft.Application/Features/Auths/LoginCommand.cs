using MediatR;

namespace ShepidiSoft.Application.Features.Auths;

public sealed record LoginCommand(string Email, string Password)
    : IRequest<ServiceResult<LoginCommandResponse>>;
