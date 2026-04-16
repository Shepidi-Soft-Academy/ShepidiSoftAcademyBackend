using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Persistence.OrganizationPositions;

public sealed class OrganizationPositionConfiguration : IEntityTypeConfiguration<OrganizationPosition>
{
    public void Configure(EntityTypeBuilder<OrganizationPosition> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Title)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(x => x.IsActive)
            .IsRequired()
            .HasDefaultValue(true);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Relationship: OrganizationPosition -> OrganizationMemberPositions (One-to-Many)
        builder.HasMany(x => x.Members)
            .WithOne(m => m.OrganizationPosition)
            .HasForeignKey(m => m.OrganizationPositionId)
            .OnDelete(DeleteBehavior.Restrict);
            
        // Unique Constraint: Pozisyon adı benzersiz olmalı
        builder.HasIndex(x => x.Title)
            .IsUnique();

        // Index'ler
        builder.HasIndex(x => x.IsActive);

        // Composite index - Aktif pozisyonlar
        builder.HasIndex(x => new { x.IsActive, x.Title });
    }
}