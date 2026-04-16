using MediatR;

namespace ShepidiSoft.Application.Features.OrganizationPositions.Queries.GetOrganizationPositions;

public sealed record GetOrganizationPositionsQuery : IRequest<ServiceResult<List<GetOrganizationPositionsQueryResponse>>>;
