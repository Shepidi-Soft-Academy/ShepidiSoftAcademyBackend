using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Organizations.Commands.UpdateOrganization;
using ShepidiSoft.Application.Features.Organizations.Queries.GetOrganizationDetail;

namespace ShepidiSoft.API.Controllers;

public sealed class OrganizationsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet("{id}")]
    [AllowAnonymous] 
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetOrganizationDetailQuery(id), cancellationToken));

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrganizationCommand command, CancellationToken cancellationToken)
    {
        if (id != command.Id)
            return BadRequest("URL'deki ID ile istek gövdesindeki (body) ID uyuşmuyor.");

        return CreateActionResult(await _mediator.Send(command, cancellationToken));
    }
}