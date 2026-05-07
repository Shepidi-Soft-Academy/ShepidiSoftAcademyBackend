using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.API.Requests;
using ShepidiSoft.Application.Features.CommunityServices.Commands.CreateCommunityService;
using ShepidiSoft.Application.Features.CommunityServices.Commands.DeleteCommunityService;
using ShepidiSoft.Application.Features.CommunityServices.Commands.UpdateCommunityService;
using ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

namespace ShepidiSoft.API.Controllers;


public class CommunityServicesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Create(
    CreateCommunityServiceCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCommunityServiceCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateCommunityServiceRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateCommunityServiceCommand(
            Id: id,
            Title: request.Title,
            Description: request.Description,
            IsActive: request.IsActive,
            ImageUrl: request.ImageUrl
            );

        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }


    [HttpGet("admin")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllForAdmin(CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetCommunityServiceListAdminQuery(), cancellationToken));


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetCommunityServiceListQuery(), cancellationToken));


}
