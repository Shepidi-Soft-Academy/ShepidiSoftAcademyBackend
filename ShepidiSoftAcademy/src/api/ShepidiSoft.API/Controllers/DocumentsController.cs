using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.API.Requests;
using ShepidiSoft.Application.Features.Documents.Commands.ChangeStatus;
using ShepidiSoft.Application.Features.Documents.Commands.CreateDocument;
using ShepidiSoft.Application.Features.Documents.Queries.GetAllDocumentsQuery;
using ShepidiSoft.Application.Features.Documents.Queries.GetDocumentListAdmin;
using ShepidiSoft.Application.Features.Documents.Queries.GetDocumentsByStatusQuery;
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




    [HttpPatch("change-status")]
     [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ChangeStatus([FromBody] ChangeDocumentStatusCommand command, CancellationToken cancellationToken)
    {
        var result = await mediator.Send(command);
        return CreateActionResult(await mediator.Send(command,cancellationToken));
    }

    
}
