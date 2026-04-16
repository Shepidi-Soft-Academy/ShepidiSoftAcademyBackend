public sealed record GetStudentsByCourseQueryResponse (
    Guid Id,
    Guid UserId,
    string Name,
    string Surname,
    string Mail
);