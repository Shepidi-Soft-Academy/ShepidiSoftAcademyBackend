using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.Application.Features.DocumentTopics.Command.CreateDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Command.DeleteDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Commands.UpdateDocumentTopic;
using ShepidiSoft.Application.Features.DocumentTopics.Queries.GetAllDocumentTopicQuery;
using ShepidiSoft.Application.Features.DocumentTopics.Queries.GetByIdDocumentTopicQuery;

[ApiController]
[Route("api/[controller]")]
public class DocumentTopicsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllDocumentTopicsQuery());
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await mediator.Send(new GetDocumentTopicByIdQuery(id));
        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateDocumentTopicCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateDocumentTopicCommand command)
    {
        var result = await mediator.Send(command);
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await mediator.Send(new DeleteDocumentTopicCommand(id));
        return Ok(result);
    }
}