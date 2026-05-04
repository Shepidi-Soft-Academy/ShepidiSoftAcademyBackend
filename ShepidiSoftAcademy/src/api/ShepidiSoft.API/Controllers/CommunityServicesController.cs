using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;

namespace ShepidiSoft.API.Controllers;


public class CommunityServicesController(IMediator mediator) : BaseApiController(mediator)
{
    public async Task<IActionResult> Create(
CreateCommunityServiceCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));



}
