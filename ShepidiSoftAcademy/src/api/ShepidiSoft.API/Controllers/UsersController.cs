using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Users.Commands.UpdatePassword;

namespace ShepidiSoft.API.Controllers;


public sealed class UsersController(IMediator mediator) : BaseApiController(mediator)
{
   
    [HttpPut("update-password")]
    public async Task<IActionResult> UpdatePassword(
    UpdatePasswordCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));

}
