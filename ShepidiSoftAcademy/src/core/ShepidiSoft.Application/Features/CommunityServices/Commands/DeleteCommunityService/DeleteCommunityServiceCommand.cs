using MediatR;

namespace ShepidiSoft.Application.Features.CommunityServices.Commands.DeleteCommunityService;

public sealed record DeleteCommunityServiceCommand(int Id) : IRequest<ServiceResult>;
