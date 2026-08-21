using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.API.Requests;
using ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus;
using ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;
using ShepidiSoft.Application.Features.Documents.Commands.DeleteDocument;
using ShepidiSoft.Application.Features.Documents.Queries.GetAllDocumentsQuery;
using ShepidiSoft.Application.Features.Documents.Queries.GetDocumentListAdmin;
using ShepidiSoft.Application.Features.Documents.Queries.GetDocumentsByStatusQuery;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API.Controllers;


public sealed class DocumentsController(IMediator mediator) : BaseApiController(mediator)
{
    
     [HttpGet]
     [AllowAnonymous]

    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllDocumentsQuery());
        return Ok(result);
    }

    [HttpGet("admin-documents")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetDocumentsAdmin()
    {
        var result = await mediator.Send(new GetDocumentListAdminQuery());
        return Ok(result);
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(new DeleteDocumentCommand(id), cancellationToken);
        return CreateActionResult(result);
    }

    // Admin Statüye göre filtreler
    [HttpGet("by-status")]
    [Authorize(Roles = "Admin")]

    public async Task<IActionResult> GetByStatus([FromQuery] DocumentStatus status)
    {
        var result = await mediator.Send(new GetDocumentsByStatusQuery(status));
        return Ok(result);
    }


    // yeni doküman oluşturma
    [HttpPost]
    [Authorize(Roles = "Admin,Student,Instructor")]
    public async Task<IActionResult> Create(
    [FromForm] CreateDocumentCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));




    [HttpPatch("change-status/{id}")]
     [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeStatus(int id,[FromBody] UpdateDocumentStatusRequest request, CancellationToken cancellationToken)
    {
        var command = new ChangeDocumentStatusCommand(id,request.NewStatus);
        return CreateActionResult(await mediator.Send(command,cancellationToken));
    }

    
}
