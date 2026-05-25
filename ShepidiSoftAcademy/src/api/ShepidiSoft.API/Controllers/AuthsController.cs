using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Auths;
using ShepidiSoft.Application.Features.Auths.ForgotPassword.Commands;
using ShepidiSoft.Application.Features.Auths.ResetPassword.Commands;

namespace ShepidiSoft.API.Controllers;

[AllowAnonymous]
public sealed class AuthsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Login(LoginCommand request, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpPost("forgot-password")]
    public async Task<IActionResult> ForgotPassword(ForgotPasswordCommand request, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPassword(ResetPasswordCommand request, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(request, cancellationToken));
}

