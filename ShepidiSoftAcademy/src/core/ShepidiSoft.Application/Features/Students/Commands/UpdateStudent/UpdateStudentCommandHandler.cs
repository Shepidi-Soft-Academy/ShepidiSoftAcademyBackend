using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.Students.Commands.UpdateStudent;

public sealed class UpdateStudentCommandHandler(
    IStudentRepository studentRepository,
    IUserService userService

    ) : IRequestHandler<UpdateStudentCommand, ServiceResult>
{
    public Task<ServiceResult> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
