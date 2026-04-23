using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;
using ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;
using ShepidiSoft.Application.Features.Meetings.Commands.DeleteMeeting;

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
}
