using AutoMapper;
using MediatR;
using ShepidiSoft.Application;
using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Enums;
using ShepidiSoft.Application.Features.OrganizationMembers.Commands.CreateOrganizationMember;
using ShepidiSoft.Domain.Entities.Organizations;
using System.Net;

public sealed class CreateOrganizationMemberCommandHandler(
    IOrganizationMemberRepository organizationMemberRepository,
    IOrganizationPositionRepository organizationPositionRepository,
    IUnitOfWork unitOfWork,
    IUserService userService,
    IMapper mapper,
    IRoleService roleService
) : IRequestHandler<CreateOrganizationMemberCommand, ServiceResult<Guid>>
{
    public async Task<ServiceResult<Guid>> Handle(
        CreateOrganizationMemberCommand request,
        CancellationToken cancellationToken)
    {
        Guid userId;

        if (request.UserId.HasValue)
        {
            var userExistResult = await userService.IsExistAsync(request.UserId.Value);

            if (!userExistResult.IsSuccess)
                return ServiceResult<Guid>.Fail(userExistResult.ErrorMessage);

            if (!userExistResult.Data)
                return ServiceResult<Guid>.Fail(
                    $"User '{request.UserId}' bulunamadı.",
                    HttpStatusCode.NotFound);

            userId = request.UserId.Value;
        }
        else
        {
            var userResult = await userService.CreateAsync(request.CreateUserRequest!);

            if (!userResult.IsSuccess)
                return ServiceResult<Guid>.Fail(
                    userResult.ErrorMessage!,
                    userResult.StatusCode);

            userId = userResult.Data!.Id;
        }

        bool alreadyMember = await organizationMemberRepository
            .AnyAsync(x => x.UserId == userId);

        if (alreadyMember)
            return ServiceResult<Guid>.Fail(
                $"User '{userId}' zaten member olarak kayıtlı.",
                HttpStatusCode.Conflict);

        var validPositionCount = await organizationPositionRepository
            .CountAsync(x => request.PositionIds.Contains(x.Id) && x.IsActive);

        if (validPositionCount != request.PositionIds.Count)
            return ServiceResult<Guid>.Fail(
                "Bazı pozisyonlar bulunamadı veya aktif değil.",
                HttpStatusCode.NotFound);

        var member = mapper.Map<OrganizationMember>(request);
        member.UserId = userId;

        await organizationMemberRepository.AddAsync(member);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        // Rol ataması
        await roleService.AssignRoleToUserAsync(userId, AppRoles.Admin);

        return ServiceResult<Guid>.Success(member.Id, HttpStatusCode.Created);
    }
}