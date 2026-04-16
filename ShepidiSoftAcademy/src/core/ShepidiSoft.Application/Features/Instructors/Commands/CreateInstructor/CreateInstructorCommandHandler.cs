using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Enums;
using ShepidiSoft.Application.Features.Outbox;
using ShepidiSoft.Domain.Entities;
using System.Net;
using System.Text.Json;
using System.Transactions;

namespace ShepidiSoft.Application.Features.Instructors.Commands.CreateInstructor;

public sealed class CreateInstructorCommandHandler(
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IMapper mapper,
    IRoleService roleService,
    IOutboxRepository outboxRepository
    ) : IRequestHandler<CreateInstructorCommand, ServiceResult<CreateInstructorCommandResponse>>
{
    public async Task<ServiceResult<CreateInstructorCommandResponse>> Handle(
       CreateInstructorCommand request,
       CancellationToken cancellationToken)
    {
        using var transactionScope = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled);

        // Create User Registiration
        var userResult = await userService.CreateAsync(request.CreateUserCommand);

        if (!userResult.IsSuccess)
            return ServiceResult<CreateInstructorCommandResponse>
                .Fail(userResult.ErrorMessage!, userResult.StatusCode);

        var userId = userResult.Data!.Id;

        //  Create Instructor Registiration
        var isInstructorExist = await instructorRepository.CheckIsInstructorExistByUserId(userId);

        if (isInstructorExist.Data)
            return ServiceResult<CreateInstructorCommandResponse>
                .Fail("Bu eğitmen zaten kayıtlı!", HttpStatusCode.BadRequest);

        var instructor = mapper.Map<Instructor>(request);
        instructor.UserId = userId;

        await instructorRepository.AddAsync(instructor);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        await roleService.AssignRoleToUserAsync(userId, AppRoles.Instructor);

        // Outbox: mail gönderimini background job'a bırak
        var logoPath = Path.Combine(AppContext.BaseDirectory, "wwwroot", "images", "logo.png");

        var emailPayload = new EmailOutboxPayload
        {
            To = request.CreateUserCommand.Email,
            Subject = "Aramıza Hoş Geldiniz",
            TemplateName = "InstructorRegister",
            Variables = new Dictionary<string, string>
            {
                { "FullName", request.CreateUserCommand.FirstName + " " + request.CreateUserCommand.LastName }
            },
            LogoPath = logoPath
        };

        var outboxMessage = new OutboxMessage
        {
            Type = "Email",
            Payload = JsonSerializer.Serialize(emailPayload)
        };

        await outboxRepository.AddAsync(outboxMessage, cancellationToken);
        await outboxRepository.SaveChangesAsync(cancellationToken);

        transactionScope.Complete();

        return ServiceResult<CreateInstructorCommandResponse>
            .Success(new CreateInstructorCommandResponse(instructor.Id));
    }
}