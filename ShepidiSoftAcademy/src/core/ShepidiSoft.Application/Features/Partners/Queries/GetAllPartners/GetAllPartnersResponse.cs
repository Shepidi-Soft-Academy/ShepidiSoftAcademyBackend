
namespace ShepidiSoft.Application.Features.Partners.Queries.GetAllPartners;

public sealed record GetAllPartnersResponse(
    int Id,
    string Name,
    string Logo,
    string WebUrl);