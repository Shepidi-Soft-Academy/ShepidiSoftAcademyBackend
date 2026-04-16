using MediatR;

namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingList;

public sealed record GetOfferingListQuery() : IRequest<ServiceResult<List<GetOfferingListQueryResponse>>>;
