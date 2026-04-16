using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShepidiSoft.Domain.Entities.Organizations;

namespace ShepidiSoft.Persistence.Organizations;

public sealed class OrganizationConfiguration : IEntityTypeConfiguration<Organization>
{
    public void Configure(EntityTypeBuilder<Organization> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .IsRequired()
            .HasMaxLength(300);

        builder.Property(x => x.Email)
            .IsRequired()
            .HasMaxLength(256);

        builder.Property(x => x.LogoUrl)
            .HasMaxLength(1000);

        builder.Property(x => x.Created)
            .IsRequired();

        builder.Property(x => x.Updated);

        // Unique Constraint: Organization adı benzersiz olmalı
        builder.HasIndex(x => x.Name)
            .IsUnique();

        // Unique Constraint: Email benzersiz olmalı
        builder.HasIndex(x => x.Email)
            .IsUnique();
    }
}