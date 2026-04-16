namespace ShepidiSoft.Application.Features.GetAnalytics.Queries.GetDashboard;

public sealed record GetDashboardAnalyticsQueryResponse(
    int TotalCourses,
    int TotalStudents,
    int TotalInstructors,
    int TotalOrganizationMember
    );
