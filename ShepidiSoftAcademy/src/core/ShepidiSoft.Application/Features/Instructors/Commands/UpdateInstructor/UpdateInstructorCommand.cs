using MediatR;

namespace ShepidiSoft.Application.Features.Instructors.Commands.UpdateInstructor;

public sealed record UpdateInstructorCommand(
    int Id,
    string Bio,
    string Expertise,
    bool IsActive
    ) : IRequest<ServiceResult>;
