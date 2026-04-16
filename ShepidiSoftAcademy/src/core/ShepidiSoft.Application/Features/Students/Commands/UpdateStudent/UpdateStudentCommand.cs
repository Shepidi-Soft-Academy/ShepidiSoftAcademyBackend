using MediatR;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.Students.Commands.UpdateStudent;

public sealed record UpdateStudentCommand(
    string University,
    string Department,
    UpdateUserRequest UpdateUserRequest
    ) : IRequest<ServiceResult>;
