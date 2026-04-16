using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplicationDetail;

public sealed record GetCareerApplicationDetailQueryResponse(
    int Id,
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? LinkedInUrl,
    string? GithubUrl,
    string CoverLetter,
    string? CvUrl,
    string OrganizationPositionName,
    string AdminNote,
    ApplicationStatus Status,
    DateTime CreatedDate,
    DateTime? UpdatedDate,
    string? UpdatedByName
    );
