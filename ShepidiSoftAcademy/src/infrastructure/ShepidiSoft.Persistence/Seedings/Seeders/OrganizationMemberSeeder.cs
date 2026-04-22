using ShepidiSoft.Application.Contracts.Identity;
using ShepidiSoft.Application.Contracts.Persistence;
using ShepidiSoft.Application.Features.Users.Dtos;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Persistence.Seedings.Seeders;

public class OrganizationMemberSeeder(
    IUserService userService,
    IRoleService roleService,
    IOrganizationMemberRepository organizationMemberRepository,
    IOrganizationRepository organizationRepository)
{
    public async Task SeedAsync()
    {
        // Admin kullanıcısı oluştur
        var adminUserRequest = new CreateUserRequest(
            FirstName: "Admin",
            LastName: "User",
            Email: "admin@shepidisoft.com",
            PhoneNumber: "+905551234567",
            DateOfBirth: new DateTime(1990, 1, 1),
            LinkednUrl: null,
            PhotoUrl: null,
            GithubUrl: null,
            YoutubeUrl: null
        );

        var userResult = await userService.CreateAsync(adminUserRequest);

        if (!userResult.IsSuccess)
            return; // User oluşturulamadı

        var adminUserId = userResult.Data!.Id;

        // Admin rolü ata
        await roleService.AssignRoleToUserAsync(adminUserId, "Admin");

        // Varsayılan organizasyon al veya oluştur
        var organizations = await organizationRepository.GetAllAsync();

        if (!organizations.Any())
        {
            // Varsayılan organizasyon oluştur
            var organization = new Organization
            {
                Name = "Shepidi Soft Academy",
                Email = "info@shepidisoft.com"
            };

            await organizationRepository.AddAsync(organization);
        }

        // OrganizationMember kaydı oluştur
        var existingMembers = await organizationMemberRepository.GetAllAsync();
        var adminMemberExists = existingMembers.Any(x => x.UserId == adminUserId);

        if (!adminMemberExists)
        {
            var adminOrgMember = new OrganizationMember
            {
                UserId = adminUserId,
                Positions = new List<OrganizationMemberPosition>()
            };

            await organizationMemberRepository.AddAsync(adminOrgMember);
        }
    }
}
