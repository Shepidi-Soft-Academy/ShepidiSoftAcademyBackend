using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.CreateCollaborationApplication;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.DeleteCollaborationApplication;
using ShepidiSoft.Application.Features.CollaborationApplications.Commands.UpdateCollaborationApplicationStatus;
using ShepidiSoft.Application.Features.CollaborationApplications.Queries.GetCollaborationApplicationList;

namespace ShepidiSoft.API.Controllers;


public sealed class CollaborationApplicationsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(
        CreateCollaborationApplicationCommand request,
        CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(request, cancellationToken));


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCollaborationApplicationCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken)); 
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateCollaborationStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCollaborationApplicationStatusCommand
        (
            Id: id,
            Status: request.Status

        );

        return CreateActionResult(await _mediator.Send(command, cancellationToken));
    }

    [HttpGet]
    [Authorize(Roles = "OrganizationMember,Admin")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    => CreateActionResult(
    await _mediator.Send(new GetCollaborationApplicationListQuery(), cancellationToken));


}
