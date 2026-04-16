
using MediatR;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.CareerApplications.Commands.UpdateCareerApplicationStatus;

public sealed record UpdateCareerApplicationStatusCommand(
    int Id,
    ApplicationStatus Status,
    string? AdminResponse
    ) : IRequest<ServiceResult>;
