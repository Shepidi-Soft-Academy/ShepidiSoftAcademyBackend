using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Activities.Queries.GetActivityDetail;
using ShepidiSoft.Application.Features.Offerings.Commands.CreateOffering;
using ShepidiSoft.Application.Features.Offerings.Commands.DeleteOffering;
using ShepidiSoft.Application.Features.Offerings.Commands.UpdateOffering;
using ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingDetail;
using ShepidiSoft.Application.Features.Offerings.Queries.GetOfferingList;

namespace ShepidiSoft.API.Controllers;

public sealed class OfferingsController(IMediator mediator) : BaseApiController(mediator)
{

    [HttpPost]
    public async Task<IActionResult> Create(CreateOfferingCommand request,CancellationToken cancellationToken)
=> CreateActionResult(await _mediator.Send(request, cancellationToken));

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteOfferingCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }

    [HttpPut("{id}")]

    public async Task<IActionResult> Update(int id, UpdateOfferingRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateOfferingCommand(
            Id: id,
            Title: request.Title,
            Description:request.Description,
            IsActive:request.IsActive
            );

        var result = await _mediator.Send(command, cancellationToken);
        return CreateActionResult(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
   => CreateActionResult(
       await _mediator.Send(new GetOfferingListQuery(), cancellationToken)); 

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
   => CreateActionResult(
       await _mediator.Send(new GetOfferingDetailQuery(id), cancellationToken));




}
