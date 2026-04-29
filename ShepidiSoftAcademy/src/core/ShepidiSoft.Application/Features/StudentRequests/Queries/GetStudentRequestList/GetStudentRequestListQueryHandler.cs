using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetStudentRequestList;

public sealed class GetStudentRequestListQueryHandler(
    IStudentRequestRepository studentRequestRepository,
    ICurrentUserService currentUserService,
    IMapper mapper
    ) : IRequestHandler<GetStudentRequestListQuery, ServiceResult<List<GetStudentRequestListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetStudentRequestListQueryResponse>>> Handle(GetStudentRequestListQuery request, CancellationToken cancellationToken)
    {
        var userId = currentUserService.UserId;

        if (!userId.HasValue)
            return ServiceResult<List<GetStudentRequestListQueryResponse>>
                .Fail("Kullanıcı bulunamadı");

        var requests = await studentRequestRepository
            .GetByStudentIdAsync(userId.Value);
        var response = mapper.Map<List<GetStudentRequestListQueryResponse>>(requests);
        return ServiceResult<List<GetStudentRequestListQueryResponse>>.Success(response);
    }
}