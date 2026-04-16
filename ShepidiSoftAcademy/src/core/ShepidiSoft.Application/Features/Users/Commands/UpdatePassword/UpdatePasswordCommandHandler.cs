using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using System.Net;

namespace ShepidiSoft.Application.Features.Users.Commands.UpdatePassword;

public sealed class UpdatePasswordCommandHandler(
    IUserService userService,
    ICurrentUserService currentUserService
    ) : IRequestHandler<UpdatePasswordCommand, ServiceResult>
{
    // UpdatePasswordCommandHandler
    public async Task<ServiceResult> Handle(UpdatePasswordCommand request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        if (userId is null)
            return ServiceResult.Fail("Kullanıcı bulunamadı.");

        await userService.UpdatePasswordAsync(userId.Value, request.OldPassword, request.NewPassword, request.ConfirmNewPassword);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
