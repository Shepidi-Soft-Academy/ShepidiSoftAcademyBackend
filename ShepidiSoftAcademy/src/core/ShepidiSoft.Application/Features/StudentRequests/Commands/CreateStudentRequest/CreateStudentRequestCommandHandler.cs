using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Domain.Entities;
using ShepidiSoft.Domain.Entities.Enums;
using System.Net;
using System.Transactions;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.CreateStudentRequest;

public sealed class CreateStudentRequestCommandHandler(
    IUnitOfWork unitOfWork,
    IStudentRequestRepository studentRequestRepository,
    ICurrentUserService currentUserService,
    IMapper mapper
    ) : IRequestHandler<CreateStudentRequestCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(
        CreateStudentRequestCommand request, CancellationToken cancellationToken)//komut al dogrula entıty olustur db yaz 
    {
      
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);//yarım verı yazılmaz 

        try
        {
           
            var currentUserId = currentUserService.UserId;
            if (currentUserId == Guid.Empty)
                return ServiceResult<Guid>.Fail("Kullanıcı oturumu bulunamadı!", HttpStatusCode.Unauthorized);

            var userId = currentUserId.Value;
            var studentRequest = mapper.Map<StudentRequest>(request);

            
            studentRequest.StudentId = userId;
            studentRequest.StudentRequestStatus = StudentRequestStatus.Bekliyor;

           
            studentRequest.Created = DateTime.UtcNow;
            studentRequest.CreatedBy = currentUserId;


            await studentRequestRepository.AddAsync(studentRequest);
            await unitOfWork.SaveChangesAsync(cancellationToken);

          
            transactionScope.Complete();

            return ServiceResult<Guid>.Success(studentRequest.Id);
        }
        catch (Exception ex)
        {
            // TransactionScope.Complete çağrılmadığı için burada otomatik Rollback gerçekleşir.
            
            throw;
        }
    }
}