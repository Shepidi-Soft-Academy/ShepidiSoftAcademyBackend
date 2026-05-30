using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.API.Requests;
using ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus;
using ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;
using ShepidiSoft.Application.Features.Documents.Commands.UpdateDocument;
using ShepidiSoft.Application.Features.Documents.Queries.GetAllDocumentsQuery;
using ShepidiSoft.Application.Features.Documents.Queries.GetDocumentListAdmin;
using ShepidiSoft.Application.Features.Documents.Queries.GetDocumentsByStatusQuery;
using ShepidiSoft.Application.Features.Documents.Queries.GetUserDocumentsQuery;
using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API.Controllers;


public sealed class DocumentsController(IMediator mediator) : BaseApiController(mediator)
{
    
     [HttpGet]

    public async Task<IActionResult> GetAll()
    {
        var result = await mediator.Send(new GetAllDocumentsQuery());
        return Ok(result);
    }

    [HttpGet("admin-documents")]
    public async Task<IActionResult> GetDocumentsAdmin()
    {
        var result = await mediator.Send(new GetDocumentListAdminQuery());
        return Ok(result);
    }

    // Öğrenc Sadece kendi dokümanlarını listeler
    [HttpGet("my-documents")]
    public async Task<IActionResult> GetMyDocuments(string userId)
    {
        var result = await mediator.Send(new GetUserDocumentsQuery(userId));
        return Ok(result);
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
    CreateDocumentCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));


    [HttpPut("{id}")]
    [Authorize(Roles = "Admin,Student,Instructor")]
    public async Task<IActionResult> Update(int id, UpdateDocumentRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateDocumentCommand
        (
            Id: id,
            DocumentTopicId: request.DocumentTopicId,   
            Title: request.Title,
            Description: request.Description,
            FileUrl: request.FileUrl
        );

        return CreateActionResult(await _mediator.Send(command, cancellationToken));
    }

    [HttpPatch("change-status")]
     [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeDocumentStatusCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        return CreateActionResult(await mediator.Send(command,cancellationToken));
    }

    
}
