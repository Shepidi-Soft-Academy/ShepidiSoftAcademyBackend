using MediatR;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.GetAnalytics.Queries.GetDashboard;

public sealed class GetDashboardAnalyticsQueryHandler(
    IStudentRepository studentRepository,
    IInstructorRepository instructorRepository,
    ICourseRepository courseRepository, 
    IOrganizationMemberRepository organizationMemberRepository
) : IRequestHandler<GetDashboardAnalyticsQuery, ServiceResult<GetDashboardAnalyticsQueryResponse>>
{
    public async Task<ServiceResult<GetDashboardAnalyticsQueryResponse>> Handle(   //Performance improvements will be made
        GetDashboardAnalyticsQuery request,
        CancellationToken cancellationToken)
    {
        var studentTask =  await studentRepository.CountAsync();
        var instructorTask =await  instructorRepository.CountAsync();
        var courseTask = await courseRepository.CountAsync();
        var orgTask = await organizationMemberRepository.CountAsync();


        var response = new GetDashboardAnalyticsQueryResponse(
            TotalCourses: courseTask,
            TotalStudents: studentTask,
            TotalInstructors: instructorTask,
            TotalOrganizationMember: orgTask
        );

        return ServiceResult<GetDashboardAnalyticsQueryResponse>.Success(response);
    }
}