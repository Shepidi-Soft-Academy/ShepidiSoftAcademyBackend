using MediatR;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationDetail;

public sealed record GetCollaborationApplicationDetailQuery(int Id) : IRequest<ServiceResult<GetCollaborationApplicationDetailQueryResponse>>;

