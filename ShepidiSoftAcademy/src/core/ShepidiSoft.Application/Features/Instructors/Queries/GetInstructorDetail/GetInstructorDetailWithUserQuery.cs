using MediatR;

namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorDetail;

public sealed record GetInstructorDetailWithUserQuery(int Id) : IRequest<ServiceResult<GetInstructorDetailWithUserQueryResponse>>;
