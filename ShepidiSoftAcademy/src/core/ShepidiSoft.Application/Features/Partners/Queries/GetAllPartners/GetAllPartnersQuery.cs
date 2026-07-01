using MediatR;

namespace ShepidiSoft.Application.Features.Partners.Queries.GetAllPartners;

public sealed record GetAllPartnersQuery() : IRequest<ServiceResult<List<GetAllPartnersResponse>>>;