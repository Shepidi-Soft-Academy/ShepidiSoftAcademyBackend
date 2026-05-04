using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;
using ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingDetail;
using System.Net;
using System.Net.NetworkInformation;

namespace ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationDetail;

public sealed class GetCollaborationApplicationDetailQueryHandler(
    ICollaborationApplicationRepository collaborationApplicationRepository,
    IMapper mapper
    ) : IRequestHandler<GetCollaborationApplicationDetailQuery, ServiceResult<GetCollaborationApplicationDetailQueryResponse>>
{
    public async Task<ServiceResult<GetCollaborationApplicationDetailQueryResponse>> Handle(GetCollaborationApplicationDetailQuery request, CancellationToken cancellationToken)
    {

        var collaborationApplication = await collaborationApplicationRepository.GetByIdAsync(request.Id);

        if (collaborationApplication is null)
            return ServiceResult<GetCollaborationApplicationDetailQueryResponse>.Fail("Başvuru Bulunamadı", HttpStatusCode.NotFound);

        var response = mapper.Map<GetCollaborationApplicationDetailQueryResponse>(collaborationApplication);

        return ServiceResult<GetCollaborationApplicationDetailQueryResponse>.Success(response);

    }
}
