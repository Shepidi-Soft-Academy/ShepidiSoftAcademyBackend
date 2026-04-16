using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Activities.Commands.DeleteActivity;
using ShepidiSoft.Application.Features.ContactMessages.Commands.CreateContactMessage;
using ShepidiSoft.Application.Features.ContactMessages.Commands.DeleteContactMessage;
using ShepidiSoft.Application.Features.ContactMessages.Commands.MarkContactMessageAsRead;
using ShepidiSoft.Application.Features.ContactMessages.Queries.GetContactMessagesList;

namespace ShepidiSoft.API.Controllers;

public sealed  class ContactMessagesController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [AllowAnonymous]
    public async Task<IActionResult> Create(
CreateContactMessageCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));



    [HttpPatch("{id}/read")]
    public async Task<IActionResult> MarkAsRead(
    int id,
    CancellationToken cancellationToken)
    {
        var command = new MarkContactMessageAsReadCommand(id);
        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
     => CreateActionResult(
      await _mediator.Send(new GetContactMessagesListQuery(), cancellationToken));

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteContactMessageCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }
}
