using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Activities.Commands.CreateActivity;
using ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityList;

namespace ShepidiSoft.API.Controllers;

public sealed class ActivitiesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create(
CreateActivityCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));

    [AllowAnonymous]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    => CreateActionResult(
        await _mediator.Send(new GetActivityListQuery(), cancellationToken));

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id,CancellationToken cancellationToken)
    => CreateActionResult(
        await _mediator.Send(new GetActivityDetailQuery(id), cancellationToken));


    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteActivityCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id,UpdateActivityRequest request,CancellationToken cancellationToken)
    {
        var command = new UpdateActivityCommand(
            Id: id,
            Title:request.Title,
            Description: request.Description,
            Date: request.Date,
            IsOnline: request.IsOnline,
            Location: request.Location,
            OnlineMeetingUrl: request.OnlineMeetingUrl
            );

        var result = await _mediator.Send(command,cancellationToken);
        return CreateActionResult(result);
    }









}
