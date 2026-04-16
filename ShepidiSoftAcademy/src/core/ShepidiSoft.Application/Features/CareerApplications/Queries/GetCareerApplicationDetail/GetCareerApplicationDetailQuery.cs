using MediatR;

namespace ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplicationDetail;

public sealed record GetCareerApplicationDetailQuery(int Id) : IRequest<ServiceResult<GetCareerApplicationDetailQueryResponse>>;
