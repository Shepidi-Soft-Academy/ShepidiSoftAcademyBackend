using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplications;

public sealed record GetCareerApplicationsQueryResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? LinkedInUrl,
    string? GithubUrl,
    string? CvUrl,
    string OrganizationPositionName,
    ApplicationStatus Status,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    string? UpdatedByName

    );



