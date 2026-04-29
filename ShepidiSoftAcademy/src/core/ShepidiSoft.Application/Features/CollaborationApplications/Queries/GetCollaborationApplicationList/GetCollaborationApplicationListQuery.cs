using MediatR;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationList;

public sealed record GetCollaborationApplicationListQuery:IRequest<ServiceResult<List<GetCollaborationApplicationListQueryResponse>>>
{
}
