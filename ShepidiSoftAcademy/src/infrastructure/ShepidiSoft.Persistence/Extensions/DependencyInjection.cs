using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Persistence.Activities;
using ShepidiSoft.Persistence.Assignments;
using ShepidiSoft.Persistence.AssignmentSubmissions;
using ShepidiSoft.Persistence.CareerApplications;
using ShepidiSoft.Persistence.ContactMessages;
using ShepidiSoft.Persistence.Context;
using ShepidiSoft.Persistence.Courses;
using ShepidiSoft.Persistence.Instructors;
using ShepidiSoft.Persistence.Interceptors;
using ShepidiSoft.Persistence.Offerings;
using ShepidiSoft.Persistence.Options;
using ShepidiSoft.Persistence.OrganizationMembers;
using ShepidiSoft.Persistence.OrganizationPositions;
using ShepidiSoft.Persistence.Outbox;
using ShepidiSoft.Persistence.Students;

namespace ShepidiSoft.Persistence.Extensions;

public static class DependencyInjection
{
    public static IServiceCollection AddPersistenceExt(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditDbContextInterceptor>();

        services.AddDbContext<AppDbContext>((serviceProvider, options) =>
        {
            var connectionStrings = configuration
                .GetSection(ConnectionStringOption.Key)
                .Get<ConnectionStringOption>();

            options.UseSqlServer(connectionStrings!.SqlServer, sqlOptions =>
            {
                sqlOptions.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });

            options.AddInterceptors(serviceProvider.GetRequiredService<AuditDbContextInterceptor>());
        });

        AddRepositories(services);

        return services;
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IActivityRepository, ActivityRepository>();
        services.AddScoped<IOfferingRepository, OfferingRepository>();
        services.AddScoped<IInstructorRepository, InstructorRepository>();
        services.AddScoped<ICourseRepository, CourseRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<ICourseMembershipRepository, CourseMembershipRepository>();
        services.AddScoped<IStudentRepository, StudentRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IOrganizationMemberRepository, OrganizationMemberRepository>();
        services.AddScoped<IOrganizationPositionRepository, OrganizationPositionRepository>();
        services.AddScoped<IAssignmentRepository, AssignmentRepository>();
        services.AddScoped<IAssignmentSubmissionRepository, AssignmentSubmissionRepository>();
        services.AddScoped<IContactMessageRepository,ContactMessageRepository>();
        services.AddScoped<ICareerApplicationRepository,CareerApplicationRepository>();
        services.AddScoped<IOutboxRepository, OutboxRepository>();
    }
}
