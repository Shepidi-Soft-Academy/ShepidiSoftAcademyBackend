using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;
using ShepidiSoft.Application.Features.Meetings.Commands.DeleteMeeting;
using ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingDetail;
using ShepidiSoft.Application.Features.Meetings.Queries.GetMeetingList;

namespace ShepidiSoft.API.Controllers;


public class MeetingsController(IMediator mediator) : BaseApiController(mediator)
{

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
    CreateMeetingCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));



    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteMeetingCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

    [HttpGet]
    [Authorize(Roles = "OrganizationMember,Admin")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
=> CreateActionResult(
    await _mediator.Send(new GetMeetingListQuery(), cancellationToken));



    [HttpGet("{id}")]
    [Authorize(Roles = "OrganizationMember,Admin")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
=> CreateActionResult(
await _mediator.Send(new GetMeetingDetailQuery(id), cancellationToken));
}
