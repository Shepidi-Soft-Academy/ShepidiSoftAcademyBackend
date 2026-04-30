using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Enums;
using System.Net;
using System.Transactions;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.CreateStudentRequest;

public sealed class CreateStudentRequestCommandHandler(
    IUnitOfWork unitOfWork,
    IStudentRequestRepository studentRequestRepository,
    IStudentRepository studentRepository,
    ICurrentUserService currentUserService,
    IMapper mapper
) : IRequestHandler<CreateStudentRequestCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(
        CreateStudentRequestCommand request,
        CancellationToken cancellationToken)
    {
        using var transactionScope =
            new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
             
            var userId = currentUserService.UserId;

            if (userId == null || userId == Guid.Empty)
                return ServiceResult<Guid>.Fail("Kullanıcı bulunamadı", HttpStatusCode.Unauthorized);

            var studentResult = await studentRepository.GetByUserId(userId.Value);
            var student= studentResult.Data;
            if (student == null)
                return ServiceResult<Guid>.Fail(
                    "Kullanıcı bulunamadı!",
                    HttpStatusCode.BadRequest);

           
            var studentRequest = mapper.Map<StudentRequest>(request);

            
            studentRequest.StudentId = student.Id;

            studentRequest.StudentRequestStatus = StudentRequestStatus.Bekliyor;
            studentRequest.Created = DateTime.UtcNow;
            studentRequest.CreatedBy = userId;

            
            await studentRequestRepository.AddAsync(studentRequest);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            transactionScope.Complete();

            return ServiceResult<Guid>.Success(studentRequest.Id);
        }
        catch (Exception)
        {
            // rollback otomatik
            throw;
        }
    }
}