using MediatR;
using ShepidiSoft.Application.Contracts.Common;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetMyRequestList;

public sealed class GetMyRequestListQueryHandler(
    IStudentRequestRepository studentRequestRepository,
    ICurrentUserService currentUserService,
    IStudentRepository studentRepository
    ) : IRequestHandler<GetMyRequestListQuery, ServiceResult<List<GetMyRequestListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetMyRequestListQueryResponse>>> Handle(GetMyRequestListQuery request, CancellationToken cancellationToken)
    {
        var studentId = await studentRepository.GetByUserId(currentUserService.UserId.Value);

        if(studentId is null) {
            return ServiceResult<List<GetMyRequestListQueryResponse>>.Fail("Student not found",HttpStatusCode.NotFound);
        }

       
        var requests = await studentRequestRepository.GetByStudentIdAsync(studentId.Data!.Id);

        var response = requests.Select(r => new GetMyRequestListQueryResponse(
            r.Title,
            r.Description,
            r.Created.ToString("yyyy-MM-dd HH:mm:ss")
        )).ToList();

        return ServiceResult<List<GetMyRequestListQueryResponse>>.Success(response);
    }
}
