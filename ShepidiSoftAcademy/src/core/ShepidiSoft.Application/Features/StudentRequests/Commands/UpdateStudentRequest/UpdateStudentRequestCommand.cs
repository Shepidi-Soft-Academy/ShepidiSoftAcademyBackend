using MediatR;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.StudentRequests.Commands.UpdateStudentRequest;

public sealed record UpdateStudentRequestCommand(
    Guid Id,                   
    string? Title,             
    string? Description,        
    StudentRequestStatus? Status // Yeni statü (admin içil)
) : IRequest<ServiceResult<string>>;