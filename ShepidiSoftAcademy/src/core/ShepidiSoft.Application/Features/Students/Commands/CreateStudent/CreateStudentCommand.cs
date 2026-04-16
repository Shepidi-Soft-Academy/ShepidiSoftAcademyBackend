using MediatR;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.Students.Commands.CreateStudent;

public sealed record CreateStudentCommand(
    string University,
    string Department,
    CreateUserRequest CreateUserCommand
) : IRequest<ServiceResult<CreateStudentCommandResponse>>;