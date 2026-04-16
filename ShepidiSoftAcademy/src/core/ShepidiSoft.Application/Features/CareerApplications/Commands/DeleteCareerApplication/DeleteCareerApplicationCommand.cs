using MediatR;

namespace ShepidiSoft.Application.Features.CareerApplications.Commands.DeleteCareerApplication;

public sealed record DeleteCareerApplicationCommand(int Id) : IRequest<ServiceResult>;
