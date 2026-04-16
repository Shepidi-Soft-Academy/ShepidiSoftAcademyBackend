namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorDetail;

public sealed record GetInstructorDetailWithUserQueryResponse(
    int Id,
    string FullName,
    string Expertise,
    string IsActive,
    string Bio,
    string? UserPhotoUrl,
    string? Email,
    string? GithubUrl,
    string? LinkednUrl,
    string? YoutubeUrl
    );
