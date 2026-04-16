using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Enums;
using ShepidiSoft.Domain.Entities;
using System.Net;
using System.Transactions;

namespace ShepidiSoft.Application.Features.Students.Commands.CreateStudent;

public sealed class CreateStudentCommandHandler(
    IStudentRepository studentRepository,
    IMapper mapper,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IRoleService roleService
    ) : IRequestHandler<CreateStudentCommand, ServiceResult<CreateStudentCommandResponse>>
{
    public async Task<ServiceResult<CreateStudentCommandResponse>> Handle(
     CreateStudentCommand request, CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        try
        {
            //User kaydı oluştur
            var userResult = await userService.CreateAsync(request.CreateUserCommand);

            if (!userResult.IsSuccess)
                return ServiceResult<CreateStudentCommandResponse>
                    .Fail(userResult.ErrorMessage!, userResult.StatusCode);

            var userId = userResult.Data!.Id;

            //Student kaydı oluştur
            var isStudentExist = await studentRepository.CheckIsInstructorExistByUserId(userId);

            if (isStudentExist.Data)
                return ServiceResult<CreateStudentCommandResponse>
                    .Fail("Bu öğrenci zaten kayıtlı!", HttpStatusCode.BadRequest);

            var student = mapper.Map<Student>(request);
            student.UserId = userId;

            await studentRepository.AddAsync(student);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await roleService.AssignRoleToUserAsync(userId, AppRoles.Student);

            transactionScope.Complete();

            return ServiceResult<CreateStudentCommandResponse>
                .Success(new CreateStudentCommandResponse(student.Id));
        }
        catch (Exception)
        {
            // TransactionScope Complete çağrılmadığı sürece Dispose'da otomatik rollback 
            throw;
        }
    }

}
