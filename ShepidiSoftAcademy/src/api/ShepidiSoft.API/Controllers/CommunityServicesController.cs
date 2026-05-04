using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;
using ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;
using ShepidiSoft.Application.Features.CommunityServices.Commands.DeleteCommunityService;

namespace ShepidiSoft.API.Controllers;


public class CommunityServicesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create(
    CreateCommunityServiceCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCommunityServiceCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

}
