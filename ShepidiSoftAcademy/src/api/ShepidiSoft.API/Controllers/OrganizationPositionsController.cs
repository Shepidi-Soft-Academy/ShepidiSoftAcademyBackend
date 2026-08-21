using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.OrganizationPositions.Commands.CreateOrganizationPosition;
using ShepidiSoft.Application.Features.OrganizationPositions.Commands.DeleteOrganizationPosition;
using ShepidiSoft.Application.Features.OrganizationPositions.Queries.GetOrganizationPositions;

namespace ShepidiSoft.API.Controllers;

public class OrganizationPositionsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
   [FromBody] CreateOrganizationPositionCommand request,
   CancellationToken cancellationToken)
   => CreateActionResult(await mediator.Send(request, cancellationToken));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrganizationPositionCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
=> CreateActionResult(
    await _mediator.Send(new GetOrganizationPositionsQuery(), cancellationToken));

}
