using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.StudentRequests.Commands.CreateStudentRequest;
using ShepidiSoft.Application.Features.StudentRequests.Commands.DeleteStudentRequest;
using ShepidiSoft.Application.Features.StudentRequests.Commands.UpdateStudentRequest;
using ShepidiSoft.Application.Features.StudentRequests.Queries.GetMyRequestList;
using ShepidiSoft.Application.Features.StudentRequests.Queries.GetRequestsByStatus;
using ShepidiSoft.Application.Features.StudentRequests.Queries.GetStudentRequestList;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API.Controllers;


public sealed class StudentRequestsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> Create(
        CreateStudentRequestCommand request,
        CancellationToken cancellationToken)
        => CreateActionResult(await mediator.Send(request, cancellationToken));

    [HttpGet("my-requests")]
    [Authorize(Roles = "Student")]
    public async Task<IActionResult> GetMyRequests(CancellationToken cancellationToken)
        => CreateActionResult(await mediator.Send(new GetMyRequestListQuery(), cancellationToken));

    [HttpGet("filter-by-status/{status}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetByStatus(
        StudentRequestStatus status,
        CancellationToken cancellationToken)
        => CreateActionResult(await mediator.Send(new GetRequestsByStatusQuery(status), cancellationToken));

    [HttpGet("all-requests")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllRequests(CancellationToken cancellationToken)
    => CreateActionResult(await mediator.Send(new GetStudentRequestListQuery(), cancellationToken));

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(UpdateStudentRequestCommand command)
    => CreateActionResult(await mediator.Send(command));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(Guid id)
        => CreateActionResult(await mediator.Send(new DeleteStudentRequestCommand(id)));
}