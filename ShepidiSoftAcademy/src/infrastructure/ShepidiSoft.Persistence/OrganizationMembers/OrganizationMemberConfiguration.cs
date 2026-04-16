using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Persistence.OrganizationMembers;

public sealed class OrganizationMemberConfiguration : IEntityTypeConfiguration<OrganizationMember>
{
    public void Configure(EntityTypeBuilder<OrganizationMember> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.UserId)
            .IsRequired();

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: OrganizationMember -> OrganizationMemberPositions (One-to-Many)
        builder.HasMany(x => x.Positions)
            .WithOne(p => p.OrganizationMember)
            .HasForeignKey(p => p.OrganizationMemberId)
            .OnDelete(DeleteBehavior.Cascade);

        // Unique Constraint: Bir kullanıcı sadece 1 member kaydına sahip olabilir
        builder.HasIndex(x => x.UserId)
            .IsUnique();

        // Index'ler
        builder.HasIndex(x => x.UserId);
        builder.HasIndex(x => x.Created);
    }
}