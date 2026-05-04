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
    IUnitOfWork unitOfWork
    ) : IRequestHandler<DeleteStudentRequestCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteStudentRequestCommand request, CancellationToken cancellationToken)
    {
        var studentRequest = await studentRequestRepository.GetByIdAsync(request.Id);

        if(studentRequest is null)
        {
            return ServiceResult.Fail("Öğrenci Talebi Bulunamadı", HttpStatusCode.NotFound);
        }
       
        studentRequestRepository.Delete(studentRequest);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}