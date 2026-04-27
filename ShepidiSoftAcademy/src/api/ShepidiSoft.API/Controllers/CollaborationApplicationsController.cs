using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;

namespace ShepidiSoft.API.Controllers;


public class CollaborationApplicationsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
CreateCollaborationApplicationCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));
}
