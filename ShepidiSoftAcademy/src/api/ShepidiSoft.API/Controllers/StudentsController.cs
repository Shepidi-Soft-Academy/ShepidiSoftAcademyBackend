using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Students.Commands.CreateStudent;
using ShepidiSoft.Application.Features.Students.Commands.DeleteStudent;
using ShepidiSoft.Application.Features.Students.Queries.GetStudentList;

namespace ShepidiSoft.API.Controllers;

public sealed class StudentsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetStudentListQuery(), cancellationToken));

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateStudentCommand request,
        CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
    {
        var command = new DeleteStudentCommand(id);
        return CreateActionResult(await _mediator.Send(command, cancellationToken));
    }
}