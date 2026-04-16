using MediatR;

namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorListWithUser;

public sealed record GetInstructorListWithUserQuery() : IRequest<ServiceResult<List<GetInstructorListWithUserQueryResponse>>>;
