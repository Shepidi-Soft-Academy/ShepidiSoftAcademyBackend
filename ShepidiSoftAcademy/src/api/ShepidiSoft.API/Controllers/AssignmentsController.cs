using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Assignments.Commands.CreateAssignment;
using ShepidiSoft.Application.Features.Assignments.Commands.DeleteAssignment;
using ShepidiSoft.Application.Features.Assignments.Commands.UpdateAssignment;
using ShepidiSoft.Application.Features.Assignments.Queries.GetMyAssignments;

namespace ShepidiSoft.API.Controllers;


public sealed class AssignmentsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    public async Task<IActionResult> Create(
CreateAssignmentCommand request,
CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteAssignmentCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }


    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, UpdateAssignmentRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateAssignmentCommand(
            Id: id,
            Title: request.Title,
            Description: request.Description,
            DueDate:request.DueDate,
            CourseId:request.CourseId
           
            );

        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }
    [HttpGet("My")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
    {
        var query = new GetMyAssignmentsQuery();
        var result = await _mediator.Send(query, cancellationToken);
        return CreateActionResult(result);
    }


}
