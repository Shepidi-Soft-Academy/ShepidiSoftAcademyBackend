using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetRequestsByStatus;

public sealed class GetRequestsByStatusQueryHandler(
    IStudentRequestRepository studentRequestRepository,
    IMapper mapper
    ) : IRequestHandler<GetRequestsByStatusQuery, ServiceResult<List<GetStudentRequestListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetStudentRequestListQueryResponse>>> Handle(GetRequestsByStatusQuery request, CancellationToken cancellationToken)
    {
        // Repository'deki diğer özel metodunu kullanıyoruz
        var requests = await studentRequestRepository.GetByStatusAsync(request.Status);
        var response = mapper.Map<List<GetStudentRequestListQueryResponse>>(requests);
        return ServiceResult<List<GetStudentRequestListQueryResponse>>.Success(response);
    }
}