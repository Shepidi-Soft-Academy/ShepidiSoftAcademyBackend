namespace ShepidiSoft.API.Requests;

public sealed record UpdateInstructorRequest(
    string Bio,
    string Expertise,
    bool IsActive
    );
