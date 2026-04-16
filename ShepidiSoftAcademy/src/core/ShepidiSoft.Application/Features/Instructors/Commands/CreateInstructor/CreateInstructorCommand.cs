using MediatR;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.Instructors.Commands.CreateInstructor;

public sealed record CreateInstructorCommand(
    string Bio,
    string Expertise,
    bool IsActive,
    CreateUserRequest CreateUserCommand
    ) : IRequest<ServiceResult<CreateInstructorCommandResponse>>;
