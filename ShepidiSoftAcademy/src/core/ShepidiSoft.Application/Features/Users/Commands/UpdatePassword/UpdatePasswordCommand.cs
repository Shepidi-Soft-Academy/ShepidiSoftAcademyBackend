using MediatR;

namespace ShepidiSoft.Application.Features.Users.Commands.UpdatePassword;

public sealed record UpdatePasswordCommand(string OldPassword,string NewPassword,string ConfirmNewPassword)
    : IRequest<ServiceResult>;


