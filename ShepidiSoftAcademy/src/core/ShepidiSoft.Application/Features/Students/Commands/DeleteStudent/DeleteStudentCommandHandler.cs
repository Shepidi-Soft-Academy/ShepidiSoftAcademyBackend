using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Notification;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;
using System.Transactions;

namespace ShepidiSoft.Application.Features.Students.Commands.DeleteStudent;

public sealed class DeleteStudentCommandHandler(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork,
    IUserService userService
) : IRequestHandler<DeleteStudentCommand, ServiceResult>
{
    public async Task<ServiceResult> Handle(DeleteStudentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // Student kaydını kontrol et
        var student = await studentRepository.GetByIdAsync(request.Id);

        if (student is null)
            return ServiceResult.Fail("Öğrenci bulunamadı!", HttpStatusCode.NotFound);

        // Student kaydını sil
        studentRepository.Delete(student);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // User kaydını sil
        var userResult = await userService.DeleteAsync(student.UserId);

        if (!userResult.IsSuccess)
            return ServiceResult.Fail(userResult.ErrorMessage!, userResult.StatusCode);

        transactionScope.Complete();

        return ServiceResult.Success(HttpStatusCode.NoContent);
    }
}
