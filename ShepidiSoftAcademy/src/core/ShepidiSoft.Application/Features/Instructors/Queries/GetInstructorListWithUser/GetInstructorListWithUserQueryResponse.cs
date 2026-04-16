namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorListWithUser;

public sealed record GetInstructorListWithUserQueryResponse(
    int Id,
    string FullName,
    string Expertise,
    string IsActive,
    string? GithubUrl,
    string? LinkednUrl,
    string? YoutubeUrl,
    string? UserPhotoUrl
    );