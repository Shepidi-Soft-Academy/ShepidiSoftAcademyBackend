using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Domain.Entities;

namespace ShepidiSoft.Application.Features.Assignments.Queries.GetMyAssignments;

public sealed class GetMyAssignmentsQueryHandler(
    IAssignmentRepository assignmentRepository,
    ICurrentUserService currentUserService,
    IRoleService roleService,
    IStudentRepository studentRepository,
    IInstructorRepository instructorRepository
) : IRequestHandler<GetMyAssignmentsQuery, ServiceResult<List<GetMyAssignmentsQueryResponse>>>
{
    public async Task<ServiceResult<List<GetMyAssignmentsQueryResponse>>> Handle(
        GetMyAssignmentsQuery request,
        CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;
        if (userId is null)
            return ServiceResult<List<GetMyAssignmentsQueryResponse>>.Fail("Kullanıcı bulunamadı.");

        var roles = await roleService.GetUserRolesAsync(userId.Value);

        IReadOnlyList<Assignment> assignments;

        if (roles.Contains("Student"))
        {
            var student = await studentRepository.GetByUserId(userId.Value);
            assignments = await assignmentRepository.GetAssignmentsForStudentAsync(student.Data.Id, cancellationToken);
        }
        else if (roles.Contains("Instructor"))
        {
            var instructor = await instructorRepository.GetInstructorIdByUserId(userId.Value);
            assignments = await assignmentRepository.GetAssignmentsForInstructorAsync(instructor.Data, cancellationToken);
        }
        else
        {
            return ServiceResult<List<GetMyAssignmentsQueryResponse>>.Fail("Kullanıcı rolü desteklenmiyor.");
        }

        var response = assignments.Select(a => new GetMyAssignmentsQueryResponse
        {
            Id = a.Id,
            Title = a.Title,
            Description = a.Description,
            DueDate = a.DueDate,
            IsActive = a.IsActive,
            CourseName = a.Course.Title,
        }).ToList();

        return ServiceResult<List<GetMyAssignmentsQueryResponse>>.Success(response);
    }
}