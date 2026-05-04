using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.Newss.Commands.CreateNewss;
using ShepidiSoft.Application.Features.Newss.Commands.DeleteNewss;
using ShepidiSoft.Application.Features.Newss.Commands.UpdateNewss;
using ShepidiSoft.Application.Features.Newss.Queries.GetNewsDetail;
using ShepidiSoft.Application.Features.Newss.Queries.GetNewsList;

namespace ShepidiSoft.API.Controllers;

public class NewsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Create(
    CreateNewsCommand request,
    CancellationToken cancellationToken)
    => CreateActionResult(await _mediator.Send(request, cancellationToken));


    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
    {
        var command = new DeleteNewsCommand(id);

        return CreateActionResult(
            await _mediator.Send(command, cancellationToken));
    }


    [HttpGet]
    [Authorize(Roles = "OrganizationMember,Admin")]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
=> CreateActionResult(
    await _mediator.Send(new GetNewsListQuery(), cancellationToken));


    [HttpGet("{slug}")]
    [Authorize(Roles = "OrganizationMember,Admin")]
    public async Task<IActionResult> GetBySlug(string slug, CancellationToken cancellationToken)
        => CreateActionResult(await _mediator.Send(new GetNewsDetailQuery(slug), cancellationToken));


   
    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Update(int id, UpdateNewsRequest request, CancellationToken cancellationToken)
    {
        var command = new UpdateNewsCommand
        (
            Id: id,
            Title: request.Title,
            Content: request.Content,
            Summary: request.Summary,
            ThumbnailUrl: request.ThumbnailUrl,
            BannerUrl: request.BannerUrl,
            IsPublished: request.IsPublished
        );

        return CreateActionResult(await _mediator.Send(command, cancellationToken));
    }
}
