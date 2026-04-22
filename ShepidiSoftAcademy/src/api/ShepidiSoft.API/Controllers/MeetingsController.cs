using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Meetings.Commands.CreateMeeting;

namespace ShepidiSoft.API.Controllers;


public class MeetingsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
CreateMeetingCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));
}
