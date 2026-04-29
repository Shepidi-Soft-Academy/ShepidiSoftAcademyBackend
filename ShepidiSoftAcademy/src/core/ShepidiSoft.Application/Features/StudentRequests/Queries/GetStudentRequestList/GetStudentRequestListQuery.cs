using MediatR;

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetStudentRequestList;

public sealed record GetStudentRequestListQuery()
    : IRequest<ServiceResult<List<GetStudentRequestListQueryResponse>>>;