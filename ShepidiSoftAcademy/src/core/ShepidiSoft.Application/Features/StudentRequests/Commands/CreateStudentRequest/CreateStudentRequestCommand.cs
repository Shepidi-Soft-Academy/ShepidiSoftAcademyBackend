using MediatR;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.CreateStudentRequest;

public sealed record CreateStudentRequestCommand(string Title, string Description)
    : IRequest<ServiceResult<Guid>>;
//mediatR ıle service Result<guid >> sonucunu dön dıyor IRequest 