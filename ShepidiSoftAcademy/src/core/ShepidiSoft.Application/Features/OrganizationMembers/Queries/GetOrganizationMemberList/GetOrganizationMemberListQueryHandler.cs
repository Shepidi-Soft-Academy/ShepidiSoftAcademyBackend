using MediatR;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;

namespace ShepidiSoft.Application.Features.OrganizationMembers.Queries.GetOrganizationMemberList;

public sealed class GetOrganizationMemberListQueryHandler(
    IOrganizationMemberRepository organizationMemberRepository,
    IUserService userService
) : IRequestHandler<GetOrganizationMemberListQuery, ServiceResult<List<GetOrganizationMemberListQueryResponse>>>
{
    public async Task<ServiceResult<List<GetOrganizationMemberListQueryResponse>>> Handle(
        GetOrganizationMemberListQuery request,
        CancellationToken cancellationToken)
    {
        
        var members = await organizationMemberRepository
            .GetAllWithPositionsAsync(cancellationToken);

        if (!members.Any())
            return ServiceResult<List<GetOrganizationMemberListQueryResponse>>.Success([]);

        
        var userIds = members.Select(x => x.UserId).Distinct().ToList();
        var userResult = await userService.GetUsersByIdsAsync(userIds, cancellationToken);

        if (!userResult.IsSuccess)
            return ServiceResult<List<GetOrganizationMemberListQueryResponse>>
                .Fail(userResult.ErrorMessage);

        var userDict = userResult.Data.ToDictionary(x => x.Id);


        var response = members
       .Select(member =>
       {
           userDict.TryGetValue(member.UserId, out var user);
           return new GetOrganizationMemberListQueryResponse(
               FullName: user is null ? string.Empty : $"{user.FirstName} {user.LastName}",
               Positions: member.Positions
                   .Select(p => p.OrganizationPosition.Title)
                   .ToList(),
               LinkednUrl: user?.LinkednUrl ?? string.Empty,
               PhotoUrl: user?.PhotoUrl ?? string.Empty
           );
       })
       .ToList();

        return ServiceResult<List<GetOrganizationMemberListQueryResponse>>.Success(response);
    }
}