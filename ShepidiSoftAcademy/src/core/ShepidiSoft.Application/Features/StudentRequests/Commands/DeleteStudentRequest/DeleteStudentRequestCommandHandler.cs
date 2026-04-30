using MediatR;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.StudentRequests.Commands.DeleteStudentRequest;
using ShepidiSoft.Domain.Entities.Enums;
using System.Net;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.DeleteStudentRequest;

public sealed class DeleteStudentRequestCommandHandler(
    IStudentRequestRepository studentRequestRepository,
    IUnitOfWork unitOfWork,
    ICurrentUserService currentUserService
    ) : IRequestHandler<DeleteStudentRequestCommand, ServiceResult<string>>
{
    public async Task<ServiceResult<string>> Handle(DeleteStudentRequestCommand request, CancellationToken cancellationToken)
    {
        var studentRequest = await studentRequestRepository.GetByIdAsync(request.Id);
        if (studentRequest == null) return ServiceResult<string>.Fail("Talep bulunamadı.");

        var userId = currentUserService.UserId;
        var isAdmin = currentUserService.IsInRole("Admin");
        var isOwner = studentRequest.StudentId == userId;

        if (!isAdmin && !isOwner)
            return ServiceResult<string>.Fail("Bu talebi silme yetkiniz yok.", HttpStatusCode.Forbidden);

        if (!isAdmin && studentRequest.StudentRequestStatus != StudentRequestStatus.Bekliyor)
            return ServiceResult<string>.Fail("İşleme alınmış talepler silinemez.");
        //admın degılse sadece beklıyor statusu sıl
        studentRequestRepository.Delete(studentRequest);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult<string>.Success("Talep başarıyla silindi.");
    }
}