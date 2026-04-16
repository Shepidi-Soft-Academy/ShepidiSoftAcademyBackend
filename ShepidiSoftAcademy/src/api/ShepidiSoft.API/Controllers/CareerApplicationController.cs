using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;
using ShepidiSoft.Application.Features.CareerApplications.Commands.CreateCareerApplication;
using ShepidiSoft.Application.Features.CareerApplications.Commands.DeleteCareerApplication;
using ShepidiSoft.Application.Features.CareerApplications.Commands.UpdateCareerApplicationStatus;
using ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplicationDetail;
using ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplications;

namespace ShepidiSoft.API.Controllers;

public class CareerApplicationController(IMediator mediator) : BaseApiController(mediator)
{
    [AllowAnonymous]
    [HttpPost]
    public async Task<IActionResult> Create(
CreateCareerApplicationCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));

    [Authorize(Roles = "Admin")]
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetCareerApplicationsQuery(), cancellationToken));


    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteCareerApplicationCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateCareerApplicationStatus request, CancellationToken cancellationToken)
    {
        var command = new UpdateCareerApplicationStatusCommand(
            Id: id,
            Status:request.Status,
            AdminResponse:request.AdminResponse
            );

        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }


    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
    => CreateActionResult(
        await _mediator.Send(new GetCareerApplicationDetailQuery(id), cancellationToken));



}

