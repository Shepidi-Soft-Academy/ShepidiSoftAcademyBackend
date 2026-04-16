

using MediatR;

namespace ShepidiSoft.Application.Features.Students.Queries.GetStudentList;

public sealed record GetStudentListQuery()
    : IRequest<ServiceResult<List<GetStudentListQueryResponse>>>;