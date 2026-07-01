using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Partners.Commands.CreatePartner;
using ShepidiSoft.Application.Features.Partners.Queries.GetAllPartners;
namespace ShepidiSoft.API.Controllers;

public sealed class PartnersController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> Create([FromForm] CreatePartnerCommand request, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
       => CreateActionResult(await _mediator.Send(new GetAllPartnersQuery(), cancellationToken));

  
}
