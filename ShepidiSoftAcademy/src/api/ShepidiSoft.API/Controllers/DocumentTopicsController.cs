using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.DocumentTopics.Command.CreateDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Command.DeleteDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Commands.UpdateDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Queries.GetAllDocumentTopicQuery;
using ShepidiSoft.Application.Features.DocumentTopics.Queries.GetByIdDocumentTopicQuery;

namespace ShepidiSoft.API.Controllers;

public class DocumentTopicsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    [AllowAnonymous]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetAllDocumentTopicsQuery(), cancellationToken));

    [HttpGet("{id}")]
    [AllowAnonymous]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetDocumentTopicByIdQuery(id), cancellationToken));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create([FromBody] CreateDocumentTopicCommand command, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(command, cancellationToken));

    [HttpPut]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update([FromBody] UpdateDocumentTopicCommand command, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(command, cancellationToken));

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new DeleteDocumentTopicCommand(id), cancellationToken));
}