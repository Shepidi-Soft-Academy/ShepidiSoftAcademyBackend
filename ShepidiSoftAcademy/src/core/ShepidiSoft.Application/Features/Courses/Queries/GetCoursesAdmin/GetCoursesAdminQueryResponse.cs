public sealed record GetCoursesAdminQueryResponse(
    int Id,
    string Title,
    bool IsOnline,
    decimal? Price,
    string Status,
    string InstructorFullName,
    string CoverImageUrl,
    string ApplicationFormUrl,

    DateTime CreatedDate,
    DateTime? LastUpdateDate,

    Guid? CreatedByUserId,
    string CreatedByUserName,
    string CreatedByEmail,

    Guid? UpdatedByUserId,
    string? UpdatedByUserName,
    string? UpdatedByEmail
);