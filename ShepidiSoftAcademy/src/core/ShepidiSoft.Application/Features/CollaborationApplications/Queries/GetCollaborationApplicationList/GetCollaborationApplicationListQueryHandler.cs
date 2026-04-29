using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationList;

public sealed class GetCollaborationApplicationListQueryHandler(
    ICollaborationApplicationRepository collaborationApplicationRepository,
    IMapper mapper
    ) : IRequestHandler<GetCollaborationApplicationListQuery, ServiceResult<List<GetCollaborationApplicationListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetCollaborationApplicationListQueryResponse>>> Handle(GetCollaborationApplicationListQuery request, CancellationToken cancellationToken)
    {

        var collaborations = await collaborationApplicationRepository.GetAllAsync();

        var response = mapper.Map<List<GetCollaborationApplicationListQueryResponse>>(collaborations);

        return ServiceResult<List<GetCollaborationApplicationListQueryResponse>>.Success(response);

    }
}

