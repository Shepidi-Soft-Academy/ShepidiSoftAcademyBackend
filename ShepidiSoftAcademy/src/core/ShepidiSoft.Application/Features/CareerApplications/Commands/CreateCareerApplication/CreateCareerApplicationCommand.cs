using MediatR;

namespace ShepidiSoft.Application.Features.CareerApplications.Commands.CreateCareerApplication;

public sealed record CreateCareerApplicationCommand(
    string FirstName,
    string LastName,
    string Email,
    string? PhoneNumber,
    string? LinkedInUrl,
    string? GithubUrl,
    string CoverLetter,
    string? CvUrl,
    int OrganizationPositionId
) : IRequest<ServiceResult<CreateCareerApplicationCommandResponse>>;

