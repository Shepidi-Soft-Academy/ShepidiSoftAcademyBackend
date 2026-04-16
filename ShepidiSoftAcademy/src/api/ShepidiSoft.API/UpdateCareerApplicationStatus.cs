using ShepidiSoft.Domain.Entities.Enums;

namespace ShepidiSoft.API;

public sealed record UpdateCareerApplicationStatus(ApplicationStatus Status,string AdminResponse);

