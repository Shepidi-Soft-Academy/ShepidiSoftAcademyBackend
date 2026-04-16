using MediatR;

namespace ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplications;

public sealed record GetCareerApplicationsQuery(

    ) : IRequest<ServiceResult<List<GetCareerApplicationsQueryResponse>>>;
