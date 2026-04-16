using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using System.Net;

namespace ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplicationDetail;

public sealed class GetCareerApplicationDetailQueryHandler(
    ICareerApplicationRepository careerApplicationRepository,
    IUserService userService
) : IRequestHandler<GetCareerApplicationDetailQuery, ServiceResult<GetCareerApplicationDetailQueryResponse>>
{
    public async Task<ServiceResult<GetCareerApplicationDetailQueryResponse>> Handle(
        GetCareerApplicationDetailQuery request,
        CancellationToken cancellationToken)
    {
        var application = await careerApplicationRepository.GetCareerApplicationWithPositionAsync(request.Id);

        if (application is null)
            return ServiceResult<GetCareerApplicationDetailQueryResponse>.Fail(
                "Başvuru bulunamadı.",
                HttpStatusCode.NotFound);

        string? updatedByName = null;

        if (application.UpdatedBy.HasValue)
        {
            var userResult = await userService.GetByIdAsync(application.UpdatedBy.Value, cancellationToken);

            if (userResult.IsSuccess && userResult.Data is not null)
                updatedByName = $"{userResult.Data.FirstName} {userResult.Data.LastName}";
        }

        var response = new GetCareerApplicationDetailQueryResponse(
            Id : request.Id,
            FirstName: application.FirstName,
            LastName: application.LastName,
            Email: application.Email,
            PhoneNumber: application.PhoneNumber,
            LinkedInUrl: application.LinkedInUrl,
            GithubUrl: application.GithubUrl,
            CoverLetter: application.CoverLetter,
            CvUrl: application.CvUrl,
            OrganizationPositionName: application.OrganizationPosition.Title,
            AdminNote: application.AdminNote,
            Status: application.Status,
            CreatedDate: application.Created,
            UpdatedDate: application.Updated,
            UpdatedByName: updatedByName
        );

        return ServiceResult<GetCareerApplicationDetailQueryResponse>.Success(response);
    }
}