using MediatR;
using Microsoft.AspNetCore.Mvc;
using ShepidiSoft.API.Abstraction;
using ShepidiSoft.Application.Features.GetAnalytics.Queries.GetDashboard;

namespace ShepidiSoft.API.Controllers;


public sealed class AnalyticsController(IMediator mediator) : BaseApiController(mediator)
{
    [HttpGet]
    public async Task<IActionResult> GetDashboardAnalytics(CancellationToken cancellationToken)
=> CreateActionResult(
    await _mediator.Send(new GetDashboardAnalyticsQuery(), cancellationToken));
}
