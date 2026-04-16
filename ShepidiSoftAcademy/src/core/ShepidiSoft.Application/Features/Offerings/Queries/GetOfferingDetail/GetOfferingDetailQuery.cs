using MediatR;

namespace ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingDetail;

public sealed record GetOfferingDetailQuery(int Id) : IRequest<ServiceResult<GetOfferingDetailQueryResponse>>;
