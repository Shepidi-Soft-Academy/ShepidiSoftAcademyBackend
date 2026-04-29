using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API.Requests;

public sealed record UpdateCareerApplicationStatus(ApplicationStatus Status,string AdminResponse);

