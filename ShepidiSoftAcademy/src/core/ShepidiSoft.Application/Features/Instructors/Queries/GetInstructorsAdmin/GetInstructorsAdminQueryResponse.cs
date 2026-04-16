namespace ShepidiSoft.Application.Features.Instructors.Queries.GetInstructorsAdmin;

public sealed record GetInstructorsAdminQueryResponse(
    int Id,
    string FullName,
    string Expertise,
    string IsActive,
    string? GithubUrl,
    string? LinkednUrl,
    string? YoutubeUrl,

    DateTime CreatedDate,
    DateTime? LastUpdateDate,

    Guid? CreatedByUserId,
    string CreatedByFullName,
    string CreatedByEmail,

    Guid? UpdatedByUserId,
    string? UpdatedByFullName,
    string? UpdatedByEmail

    );
