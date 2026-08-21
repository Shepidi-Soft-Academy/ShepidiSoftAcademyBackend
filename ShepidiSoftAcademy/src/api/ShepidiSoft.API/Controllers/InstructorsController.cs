using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.API.Requests;
using ShepidiSoft.Application.Features.Instructors.Commands.CreateInstructor;
using ShepidiSoft.Application.Features.Instructors.Commands.DeleteInstructor;
using ShepidiSoft.Application.Features.Instructors.Commands.UpdateInstructor;
using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorDetail;
using ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorListWithUser;

namespace ShepidiSoft.API.Controllers;


public sealed class InstructorsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
CreateInstructorCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteInstructorCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> Update(int id, UpdateInstructorRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateInstructorCommand(
            Id: id,
            Expertise:request.Expertise,
            Bio: request.Bio,
            IsActive: request.IsActive
            );

        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }


    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    => CreateActionResult(
        await _mediator.Send(new GetInstructorListWithUserQuery(), cancellationToken));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
=> CreateActionResult(
    await _mediator.Send(new GetInstructorDetailWithUserQuery(id), cancellationToken));





}
