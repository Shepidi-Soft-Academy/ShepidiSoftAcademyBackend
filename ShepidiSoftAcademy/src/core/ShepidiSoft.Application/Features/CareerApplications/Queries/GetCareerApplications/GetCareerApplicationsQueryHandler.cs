using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Users.Dtos;

namespace ShepidiSoft.Application.Features.CareerApplications.Queries.GetCareerApplications;

public sealed class GetCareerApplicationsQueryHandler(
    ICareerApplicationRepository careerApplicationRepository,
    IUserService userService
) : IRequestHandler<GetCareerApplicationsQuery, ServiceResult<List<GetCareerApplicationsQueryResponse>>>
{
    public async Task<ServiceResult<List<GetCareerApplicationsQueryResponse>>> Handle(
        GetCareerApplicationsQuery request,
        CancellationToken cancellationToken)
    {
        var applications = await careerApplicationRepository.GetAllCareerApplicationsWithPositionAsync();

        if (applications.Count == 0)
            return ServiceResult<List<GetCareerApplicationsQueryResponse>>.Success([]);

        var updatedByUserIds = applications
            .Where(x => x.UpdatedBy.HasValue)
            .Select(x => x.UpdatedBy!.Value)
            .Distinct()
            .ToList();

        var userLookup = new Dictionary<Guid, UserDto>();

        if (updatedByUserIds.Count != 0)
        {
            var usersResult = await userService.GetUsersByIdsAsync(updatedByUserIds, cancellationToken);

            if (usersResult.IsSuccess && usersResult.Data is not null)
                userLookup = usersResult.Data.ToDictionary(u => u.Id);
        }

        UserDto? GetUser(Guid? id)
        {
            if (!id.HasValue) return null;
            return userLookup.TryGetValue(id.Value, out var user) ? user : null;
        }

        var result = applications.Select(a =>
        {
            var updatedByUser = GetUser(a.UpdatedBy);

            return new GetCareerApplicationsQueryResponse(
                Id : a.Id,
                FirstName: a.FirstName,
                LastName: a.LastName,
                Email: a.Email,
                PhoneNumber: a.PhoneNumber,
                LinkedInUrl: a.LinkedInUrl,
                GithubUrl: a.GithubUrl,
                CvUrl: a.CvUrl,
                OrganizationPositionName: a.OrganizationPosition.Title,
                Status: a.Status,
                CreatedDate: a.Created,
                UpdatedDate: a.Updated,
                UpdatedByName: updatedByUser is not null
                    ? $"{updatedByUser.FirstName} {updatedByUser.LastName}"
                    : null
            );
        }).ToList();

        return ServiceResult<List<GetCareerApplicationsQueryResponse>>.Success(result);
    }
}