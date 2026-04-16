using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Offerings.Commands.DeleteOffering;
using ShepidiSoft.Application.Features.OrganizationMembers.Commands.CreateOrganizationMember;
using ShepidiSoft.Application.Features.OrganizationMembers.Commands.DeleteOrganizationMember;
using ShepidiSoft.Application.Features.OrganizationMembers.Commands.UpdateOrganizationMember;
using ShepidiSoft.Application.Features.OrganizationMembers.Queries.GetOrganizationMemberList;

namespace ShepidiSoft.API.Controllers;

public sealed class OrganizationMembersController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create(
    [FromBody] CreateOrganizationMemberCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await mediator.Send(request, cancellationToken));

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
      => CreateActionResult(await mediator.Send(new GetOrganizationMemberListQuery(), cancellationToken));

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateOrganizationMemberRequest request,
        CancellationToken cancellationToken)
    {
        var command = new UpdateOrganizationMemberCommand(
            Id: id,
            PositionIds: request.PositionIds
        );

        return CreateActionResult(await mediator.Send(command, cancellationToken));
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteOrganizationMemberCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }
}