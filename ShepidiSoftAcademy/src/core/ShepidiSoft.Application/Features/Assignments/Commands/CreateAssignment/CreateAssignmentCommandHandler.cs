using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Assignments.Commands.CreateAssignment;

public sealed class CreateAssignmentCommandHandler(
    IAssignmentRepository assignmentRepository,
    ICourseRepository courseRepository,
    ICurrentUserService currentUserService,
    IInstructorRepository instructorRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper
) : IRequestHandler<CreateAssignmentCommand, ServiceResult<CreateAssignmentCommandResponse>>
{
    public async Task<ServiceResult<CreateAssignmentCommandResponse>>
        Handle(CreateAssignmentCommand request, CancellationToken cancellationToken)
    {
        var course = await courseRepository.GetByIdAsync(request.CourseId);
        if (course is null)
            return ServiceResult<CreateAssignmentCommandResponse>
                .Fail("Kurs bulunamadı.");

        var userId = currentUserService.UserId;
        if (userId is null)
            return ServiceResult<CreateAssignmentCommandResponse>
                .Fail("Kullanıcı bulunamadı.");

        var instructorId = await instructorRepository.GetInstructorIdByUserId(userId.Value);

        if (course.InstructorId != instructorId.Data)
            return ServiceResult<CreateAssignmentCommandResponse>
                .Fail("Bu kursa ödev ekleme yetkiniz yok.");

        var assignment = mapper.Map<Assignment>(request);
        assignment.Created = DateTime.UtcNow;
        assignment.IsActive = true;

        await PersistAsync(assignment, cancellationToken);

        return ServiceResult<CreateAssignmentCommandResponse>
            .Success(new CreateAssignmentCommandResponse(assignment.Id));
    }

    private async Task PersistAsync(Assignment assignment, CancellationToken cancellationToken)
    {
        await assignmentRepository.AddAsync(assignment);
        await unitOfWork.SaveChangesAsync(cancellationToken);
    }
}