namespace ShepidiSoft.API;

public sealed record UpdateInstructorRequest(
    string Bio,
    string Expertise,
    bool IsActive
    );
