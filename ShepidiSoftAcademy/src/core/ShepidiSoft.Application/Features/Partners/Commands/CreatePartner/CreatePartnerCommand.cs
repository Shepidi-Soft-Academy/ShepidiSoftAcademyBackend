using MediatR;
using Microsoft.AspNetCore.Http;


namespace ShepidiSoft.Application.Features.Partners.Commands.CreatePartner;
public sealed record CreatePartnerCommand(
string Name,string WebsiteUrl, IFormFile Logo) : IRequest<ServiceResult<CreatePartnerResponse>>;

