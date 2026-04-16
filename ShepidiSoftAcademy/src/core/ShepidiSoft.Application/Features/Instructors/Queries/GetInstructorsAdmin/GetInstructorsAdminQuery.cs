using MediatR;

namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorsAdmin;

public sealed record GetInstructorsAdminQuery
    : IRequest<ServiceResult<List<GetInstructorsAdminQueryResponse>>>;
