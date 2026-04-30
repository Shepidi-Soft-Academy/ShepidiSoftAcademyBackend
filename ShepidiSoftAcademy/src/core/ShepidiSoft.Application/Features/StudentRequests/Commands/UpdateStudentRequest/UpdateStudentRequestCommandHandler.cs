using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.StudentRequests.Commands.UpdateStudentRequest;
using ShepidiSoft.Domain.Entities.Enums;
using System.Net;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.UpdateStudentRequest;

public sealed class UpdateStudentRequestCommandHandler(
    IStudentRequestRepository studentRequestRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService
    ) : IRequestHandler<UpdateStudentRequestCommand, ServiceResult<string>>
{
    public async Task<ServiceResult<string>> Handle(UpdateStudentRequestCommand request, CancellationToken cancellationToken)
    {
        var studentRequest = await studentRequestRepository.GetByIdAsync(request.Id);
        if (studentRequest == null) return ServiceResult<string>.Fail("Talep bulunamadı.");

        var isAdmin = currentUserService.IsInRole("Admin");
        var isOwner = studentRequest.StudentId == currentUserService.UserId;

        if (isAdmin)
        {
            if (request.Status.HasValue)
                studentRequest.StudentRequestStatus = request.Status.Value;
        }
        else if (isOwner)
        {
            if (studentRequest.StudentRequestStatus != StudentRequestStatus.Bekliyor)
                return ServiceResult<string>.Fail("Onaylanmış talepler güncellenemez.");

            studentRequest.Title = request.Title ?? studentRequest.Title;
            studentRequest.Description = request.Description ?? studentRequest.Description;
        }
        else
        {
            return ServiceResult<string>.Fail("Yetkisiz erişim.", HttpStatusCode.Forbidden);
        }

        studentRequestRepository.Update(studentRequest);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<string>.Success("Güncelleme yapıldı.");
    }
}