using ShepidiSoft.Persistence.Seedings.Seeders;

namespace ShepidiSoft.Persistence.Seedings;

public class DatabaseSeeder : IDatabaseSeeder
{
    private readonly RoleSeeder _roleSeeder;
    private readonly OrganizationMemberSeeder _organizationMemberSeeder;

    public DatabaseSeeder(RoleSeeder roleSeeder, OrganizationMemberSeeder organizationMemberSeeder)
    {
        _roleSeeder = roleSeeder;
        _organizationMemberSeeder = organizationMemberSeeder;
    }

    public async Task SeedAsync()
    {
        // Rolleri seed et
        await _roleSeeder.SeedAsync();

        // OrganizationMember'ı seed et
        await _organizationMemberSeeder.SeedAsync();
    }
}
