using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.Application.Features.Organizations.Commands.UpdateOrganization;
using ShepidiSoft.Application.Features.Organizations.Queries.GetOrganizationDetail;
using System.Net;

namespace ShepidiSoft.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")] 
public sealed class OrganizationsController(IMediator mediator) : ControllerBase
{
 
    [HttpGet("{id}")]
    [AllowAnonymous] 
    public async Task<IActionResult> Get([FromRoute] int id, CancellationToken cancellationToken)
    {
        
        var result = await mediator.Send(new GetOrganizationDetailQuery(id), cancellationToken);

        if (!result.IsSuccess)
        {
            return result.StatusCode == HttpStatusCode.NotFound
                ? NotFound(result)
                : BadRequest(result);
        }

        return Ok(result);
    }

  
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")] 
    public async Task<IActionResult> Update([FromRoute] int id, [FromBody] UpdateOrganizationCommand command, CancellationToken cancellationToken)
    {
       
        if (id != command.Id)
        {
            return BadRequest("URL'deki ID ile istek gövdesindeki (body) ID uyuşmuyor.");
        }

        var result = await mediator.Send(command, cancellationToken);

        if (!result.IsSuccess)
        {
            return result.StatusCode == HttpStatusCode.NotFound
                ? NotFound(result)
                : BadRequest(result);
        }


        return NoContent();
    }
}