using MediatR;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.StudentRequests.Queries.GetRequestsByStatus;

public sealed record GetRequestsByStatusQuery(StudentRequestStatus Status)
    : IRequest<ServiceResult<List<GetStudentRequestListQueryResponse>>>;