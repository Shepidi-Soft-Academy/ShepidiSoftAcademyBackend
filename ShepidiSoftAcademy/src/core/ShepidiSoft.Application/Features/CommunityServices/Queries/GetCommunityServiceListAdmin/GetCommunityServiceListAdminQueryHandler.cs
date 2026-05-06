using AutoMapper;
using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.CommunityServices.Queries.GetCommunityServiceList;

public sealed class GetCommunityServiceListAdminQueryHandler(
    ICommunityServiceRepository communityServiceRepository,
    IMapper mapper,
    IUserService userService
    ) : IRequestHandler<GetCommunityServiceListAdminQuery, ServiceResult<List<GetCommunityServiceListAdminQueryResponse>>>
{
    public async Task<ServiceResult<List<GetCommunityServiceListAdminQueryResponse>>> Handle(GetCommunityServiceListAdminQuery request, CancellationToken cancellationToken)
    {
        var communityServices = await communityServiceRepository.GetAllAsync();

        if (!communityServices.Any())
            return ServiceResult<List<GetCommunityServiceListAdminQueryResponse>>.Success([]);

        var creatorIds = communityServices
            .Where(cs => cs.CreatedBy.HasValue)
            .Select(cs => cs.CreatedBy.Value)
            .Distinct()
            .ToList();

        var usersResult = await userService.GetUsersByIdsAsync(creatorIds, cancellationToken);

        if (!usersResult.IsSuccess)
            return ServiceResult<List<GetCommunityServiceListAdminQueryResponse>>.Fail(usersResult.ErrorMessage!);

        var response = communityServices.Select(cs =>
        {
            var creator = usersResult.Data?.FirstOrDefault(u => u.Id == cs.CreatedBy);
            var createdByName = creator != null 
                ? $"{creator.FirstName} {creator.LastName}" 
                : string.Empty;

            return new GetCommunityServiceListAdminQueryResponse(
                Id: cs.Id,
                Title: cs.Title,
                IsActive: cs.IsActive,
                CreatedByName: createdByName
            );
        }).ToList();

        return ServiceResult<List<GetCommunityServiceListAdminQueryResponse>>.Success(response);
    }
}
