using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.DeleteCollaborationApplication;

namespace ShepidiSoft.API.Controllers;


public class CollaborationApplicationsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(
CreateCollaborationApplicationCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCollaborationApplicationCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }
}
