using MediatR;

namespace ShepidiSoft.Application.Features.GetAnalytics.Queries.GetDashboard;

public sealed record GetDashboardAnalyticsQuery() : IRequest<ServiceResult<GetDashboardAnalyticsQueryResponse>>;
